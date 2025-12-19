using pos_api_app.Models.Entities;
using System.Data.Common;

namespace pos_api_app.DTOs.TransactionsItemDTO;

public class NewTransactionItemDTO
{
	public int? ProductId { get; set; }
	public int? PriceId { get; set; }
	public float Quantity { get; set; }
	public decimal Subtotal { get; set; }

	// public static explicit operator TransactionItem(NewTransactionItemDTO transactionItem)
	// {
	//     return new TransactionItem
	//     {
	//         ProductId = transactionItem.ProductId,
	//         PriceId = transactionItem.PriceId,
	//         Quantity = transactionItem.Quantity,
	//         Subtotal = transactionItem.Subtotal,
	//     };
	// }
}
