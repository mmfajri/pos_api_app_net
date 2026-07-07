using pos_api_app.DTOs.GeneralDTO;

namespace pos_api_app.DTOs.TransactionsDTO;

public class GetTransactionDTO : TableDTO
{
	public string TransactionDate { get; set; } = string.Empty;
}
