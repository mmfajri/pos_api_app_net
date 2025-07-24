using pos_api_app.Model.Entities;

namespace pos_api_app.DTOs.UnitDTO;

public class UnitDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;

    public static explicit operator UnitDTO(Unit unit)
    {
        return new UnitDTO
        {
            Guid = unit.Guid,
            Name = unit.Name,
        };
    }
}
