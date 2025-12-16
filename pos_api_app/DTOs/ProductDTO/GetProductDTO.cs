using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class GetProductDTO
{
	public int PriceId { get; set; } //--> Must Later to Rename to PriceId to Avoid Confusion
	public string BarcodeId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string QuantityType { get; set; } = string.Empty;
	public decimal Amount { get; set; } = 0;

	public static explicit operator Price(GetProductDTO productDTO)
	{
		return new Price
		{
			Id = productDTO.PriceId,
			Amount = productDTO.Amount
		};
	}
	public static explicit operator Product(GetProductDTO productDTO)
	{
		return new Product
		{
			BarcodeID = productDTO.BarcodeId,
			Title = productDTO.Title,
		};
	}
	public static explicit operator Unit(GetProductDTO productDTO)
	{
		return new Unit
		{
			Name = productDTO.QuantityType
		};
	}
}
