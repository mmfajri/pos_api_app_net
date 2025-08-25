using pos_api_app.DTOs.TransactionsItemDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.TransactionsDTO;

public class TransactionDTO
{
    public int Id { get; set; }
    public int? AccountId { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? TotalAmmount { get; set; }
    public ICollection<TransactionItemDTO>? TransactionItemsDTO { get; set; }

    public static explicit operator TransactionDTO(Transaction transaction)
    {
        return new TransactionDTO
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            TransactionDate = transaction.TransactionsDate,
            TotalAmmount = transaction.TotalAmmount
        };
    }
    public static explicit operator Transaction(TransactionDTO transactionDTO)
    {
        return new Transaction
        {
            AccountId = transactionDTO.AccountId,
            TransactionsDate = transactionDTO.TransactionDate,
            TotalAmmount = transactionDTO.TotalAmmount
        };
    }
}
