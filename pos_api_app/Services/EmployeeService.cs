using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.EmployeesDTO;
using pos_api_app.DTOs.RolesDTO;
using pos_api_app.Models.Entities;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities.Enums;

namespace pos_api_app.Services;

public class EmployeeService
{
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IRoleRepository _roleRepository;
	private readonly PosDbContext _posDbContext;

	public EmployeeService(IEmployeeRepository employeeRepository,
	       IRoleRepository roleRepository,
	       PosDbContext posDbContext)
	{
		_employeeRepository = employeeRepository;
		_roleRepository = roleRepository;
		_posDbContext = posDbContext;
	}

	public async Task<IEnumerable<EmployeeDTO>?> Get()
	{
		var employeeList = await _employeeRepository.GetAll();
		if (employeeList == null || !employeeList.Any())
		{
			return null;
		}

		var dto = employeeList.Select(employee => (EmployeeDTO)employee);
		return dto;

	}

	public async Task<EmployeeDTO?> Get(int id)
	{
		var employee = await _employeeRepository.GetById(id);
		if (employee == null)
		{
			return null;
		}

		var dto = (EmployeeDTO)employee;
		return dto;
	}

	public async Task<EmployeeDTO?> Create(NewEmployeeDTO newEmployeeDTO)
	{
		using (var transactions = _posDbContext.Database.BeginTransaction())
		{
			try
			{
				//check existing role in employee's input
				var getRole = await _roleRepository.GetByName(nameof(newEmployeeDTO.RoleSet));
				Role? newRole = null;

				if (getRole == null)
				{
					newRole = (Role)new NewRoleDTO
					{
						Name = Enum.GetName(typeof(EmployeeEnum), newEmployeeDTO.RoleSet)!
					};
					newRole = await _roleRepository.Create(newRole);
				}
				var roleToUse = getRole ?? newRole;

				var newEmployee = (Employee)newEmployeeDTO;
				//newEmployee.RoleGuid = roleToUse!.Guid;

				var createdEmployee = await _employeeRepository.Create(newEmployee);
				if (createdEmployee != null)
				{
					transactions.Commit();
					return (EmployeeDTO)createdEmployee;
				}
				else
				{
					return null;
				}
			}
			catch
			{
				transactions.Rollback();
				return null;
			}
		}
	}

	public async Task<bool> Edit(EmployeeDTO employeeDTO)
	{
		using (var transaction = _posDbContext.Database.BeginTransaction())
		{
			try
			{
				//get employee
				var getEmployee = await _employeeRepository.GetById(employeeDTO.Id);
				if (getEmployee == null) return false;

				//set new employee
				var newEmployee = (Employee)employeeDTO;
				//newEmployee.RoleGuid = getEmployee.RoleGuid;

				// update the employee
				var updateEmployee = await _employeeRepository.Update(newEmployee);
				if (!updateEmployee) return false;
				transaction.Commit();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	public async Task<int> Delete(int id)
	{
		using (var transactions = _posDbContext.Database.BeginTransaction())
		{
			try
			{
				var employeeEntity = await _employeeRepository.GetById(id);
				if (employeeEntity == null) return 0;

				var isDelete = await _employeeRepository.Delete(employeeEntity);
				if (isDelete == false) return 0;
				transactions.Commit();
				return 1;

			}
			catch
			{
				transactions.Rollback();
				return -1;
			}
		}

	}
}
