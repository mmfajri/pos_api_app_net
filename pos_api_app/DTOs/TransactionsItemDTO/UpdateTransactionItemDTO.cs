namespace pos_api_app.DTOs.TransactionsItemDTO;

public class UpdateTransactionItemDTO
{
	public string? TransactionId { get; set; }
	public IEnumerable<TransactionItemDTO>? ListTransactionItem { get; set; }
}
