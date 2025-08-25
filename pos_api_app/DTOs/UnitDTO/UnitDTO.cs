using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.UnitDTO;

public class UnitDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static explicit operator UnitDTO(Unit unit)
    {
        return new UnitDTO
        {
            Name = unit.Name,
        };
    }
}
