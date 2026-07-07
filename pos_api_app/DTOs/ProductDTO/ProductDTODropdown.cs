using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class ProductDTODropdown
{
	public string BarcodeId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;

	public static explicit operator ProductDTODropdown(Product model)
	{
		return new ProductDTODropdown
		{
			BarcodeId = model.BarcodeID,
			Title = model.Title,
		};
	}
}
