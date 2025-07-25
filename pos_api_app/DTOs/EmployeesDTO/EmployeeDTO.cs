using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.EmployeesDTO;

public class EmployeeDTO
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public Guid? AccountGuid { get; set; }

    public static explicit operator Employee(EmployeeDTO dto)
    {
        return new Employee
        {
            Guid = dto.Guid,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            AccountGuid = dto.AccountGuid,
        };
    }

    public static explicit operator EmployeeDTO(Employee employee)
    {
        return new EmployeeDTO
        {
            Guid = employee.Guid,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            AccountGuid = employee.AccountGuid,
        };
    }

}
