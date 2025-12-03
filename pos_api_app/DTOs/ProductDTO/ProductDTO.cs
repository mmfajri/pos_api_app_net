using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class ProductDTO
{
	public int Id { get; set; }
	public string BarcodeId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string QuantityType { get; set; } = string.Empty;
	public decimal Amount { get; set; } = 0;


	public static explicit operator Price(ProductDTO productDTO)
	{
		return new Price
		{
			Id = productDTO.Id,
			Amount = productDTO.Amount
		};
	}
	public static explicit operator Product(ProductDTO productDTO)
	{
		return new Product
		{
			BarcodeID = productDTO.BarcodeId,
			Title = productDTO.Title,
		};
	}
	public static explicit operator Unit(ProductDTO productDTO)
	{
		return new Unit
		{
			Name = productDTO.QuantityType
		};
	}
}
