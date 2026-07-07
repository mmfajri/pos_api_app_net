using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.RolesDTO;

public class RoleDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static explicit operator Role(RoleDTO dto)
    {
        return new Role
        {
            Name = dto.Name,
        };
    }

    public static explicit operator RoleDTO(Role role)
    {
        return new RoleDTO
        {
            Id = role.Id,
            Name = role.Name,
        };
    }
}
