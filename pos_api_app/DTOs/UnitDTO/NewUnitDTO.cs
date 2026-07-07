using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.UnitDTO;

public class NewUnitDTO
{
	public string Name { get; set; } = string.Empty;

	public static explicit operator Unit(NewUnitDTO newUnitDTO)
	{
		return new Unit
		{
			Name = newUnitDTO.Name,
		};
	}

}
