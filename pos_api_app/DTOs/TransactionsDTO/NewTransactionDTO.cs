using pos_api_app.DTOs.TransactionsItemDTO;
using pos_api_app.Models.Entities;
using System.Data.Common;

namespace pos_api_app.DTOs.TransactionsDTO;

public class NewTransactionDTO
{
	public int? AccountId { get; set; }
	public DateTime TransactionDate { get; set; }
	public decimal TotalAmmount { get; set; }
	public decimal PayAmount { get; set; }
	public IEnumerable<NewTransactionItemDTO>? TransactionItemDTOs { get; set; }

	public static explicit operator Transaction(NewTransactionDTO newTransactionDTO)
	{
		return new Transaction
		{
			AccountId = newTransactionDTO.AccountId,
			TransactionsDate = newTransactionDTO.TransactionDate,
			TotalAmmount = newTransactionDTO.TotalAmmount,
			PayAmount = newTransactionDTO.PayAmount,
		};
	}
}
