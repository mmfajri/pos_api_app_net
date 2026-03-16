namespace pos_api_app.DTOs.TransactionsItemDTO;

public class UpdateTransactionItemDTO
{
	public int TransactionId { get; set; }
	public IEnumerable<NewTransactionItemDTO>? ListTransactionItem { get; set; }
}
