using pos_api_app.Models.Entities;
using pos_api_app.Utilities.Enums;

namespace pos_api_app.DTOs.EmployeesDTO;

public class NewEmployeeDTO
{
	public string Firstname { get; set; } = string.Empty;
	public string Lastname { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public EmployeeEnum RoleSet { get; set; }

	public static explicit operator Employee(NewEmployeeDTO dto)
	{
		return new Employee
		{
			Guid = Guid.NewGuid(),
			FirstName = dto.Firstname,
			LastName = dto.Lastname,
		};
	}
}
