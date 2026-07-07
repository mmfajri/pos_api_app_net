using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.EmployeesDTO;

public class EmployeeDTO
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string? LastName { get; set; }
	public int? AccountId { get; set; }

	public static explicit operator Employee(EmployeeDTO dto)
	{
		return new Employee
		{
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			AccountId = dto.AccountId,
		};
	}

	public static explicit operator EmployeeDTO(Employee employee)
	{
		return new EmployeeDTO
		{
			Id = employee.Id,
			FirstName = employee.FirstName,
			LastName = employee.LastName,
			AccountId = employee.AccountId,
		};
	}

}
