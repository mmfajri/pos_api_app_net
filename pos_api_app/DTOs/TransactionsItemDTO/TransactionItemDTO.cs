using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.TransactionsItemDTO;

public class TransactionItemDTO
{
	public string? BarcodeId { get; set; }
	public string? TitleProduct { get; set; }
	public string? QuantityType { get; set; }
	public decimal? Quantity { get; set; }
	public decimal? PriceProduct { get; set; }
	public decimal? TotalPrice { get; set; }

	public static explicit operator TransactionItemDTO(TransactionItem transactionItem)
	{
		return new TransactionItem
		{
			BarcodeId = transactionItem.BarcodeId,
			TitleProduct = transactionItem.TitleProduct,
			QuantityType = transactionItem.QuantityType,
			Quantity = transactionItem.Quantity,
			PriceProduct = transactionItem.PriceProduct,
			TotalPrice = transactionItem.TotalPrice,
		};
	}

	// public static explicit operator TransactionItem(TransactionItemDTO transactionItem)
	// {
	//     return new TransactionItem
	//     {
	//         TransactionId = transactionItem.TransactionId,
	//         ProductId = transactionItem.ProductId,
	//         PriceId = transactionItem.PriceId,
	//         Quantity = transactionItem.Quantity,
	//         Subtotal = transactionItem.SubTotal
	//     };
	// }
}
