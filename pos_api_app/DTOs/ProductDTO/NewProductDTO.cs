using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class NewProductDTO
{
	public string BarcodeID { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string QuantityType { get; set; } = string.Empty;
	public decimal Amount { get; set; } = 0m;

	public static explicit operator Product(NewProductDTO product)
	{
		return new Product
		{
			BarcodeID = product.BarcodeID,
			Title = product.Title,
		};
	}

	public static explicit operator Unit(NewProductDTO productDTO)
	{
		return new Unit
		{
			Name = productDTO.QuantityType
		};
	}
	public static explicit operator Price(NewProductDTO productDTO)
	{
		return new Price
		{
			Amount = productDTO.Amount
		};
	}
}
