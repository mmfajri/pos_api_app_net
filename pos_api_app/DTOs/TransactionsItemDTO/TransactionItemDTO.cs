using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.TransactionsItemDTO;

public class TransactionItemDTO
{
    public int Id { get; set; }
    public int? TransactionId { get; set; }
    public int? ProductId { get; set; }
    public int? PriceId { get; set; }
    public float Quantity { get; set; }
    public decimal SubTotal { get; set; }

    public static explicit operator TransactionItemDTO(TransactionItem transactionItem)
    {
        return new TransactionItemDTO
        {
            Id = transactionItem.Id,
            TransactionId = transactionItem.TransactionId,
            ProductId = transactionItem.ProductId,
            PriceId = transactionItem.PriceId,
            Quantity = transactionItem.Quantity,
            SubTotal = transactionItem.Subtotal
        };
    }

    public static explicit operator TransactionItem(TransactionItemDTO transactionItem)
    {
        return new TransactionItem
        {
            TransactionId = transactionItem.TransactionId,
            ProductId = transactionItem.ProductId,
            PriceId = transactionItem.PriceId,
            Quantity = transactionItem.Quantity,
            Subtotal = transactionItem.SubTotal
        };
    }
}
