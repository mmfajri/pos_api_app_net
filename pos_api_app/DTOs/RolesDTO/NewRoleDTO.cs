using pos_api_app.Model.Entities;

namespace pos_api_app.DTOs.RolesDTO;

public class NewRoleDTO
{
    public string Name { get; set; } = string.Empty;

    public static explicit operator Role(NewRoleDTO newRoleDTO)
    {
        return new Role
        {
            Guid = Guid.NewGuid(),
            Name = newRoleDTO.Name,
        };
    }
}
