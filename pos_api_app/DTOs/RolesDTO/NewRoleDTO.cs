using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.RolesDTO;

public class NewRoleDTO
{
	public string Name { get; set; } = string.Empty;

	public static explicit operator Role(NewRoleDTO newRoleDTO)
	{
		return new Role
		{
			Name = newRoleDTO.Name,
		};
	}
}
