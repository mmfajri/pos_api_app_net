using pos_api_app.DTOs.TransactionsItemDTO;
using pos_api_app.Model.Entities;
using System.Data.Common;

namespace pos_api_app.DTOs.TransactionsDTO;

public class NewTransactionDTO
{
    public Guid? EmployeeGuid { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal TotalAmmount { get; set; }
    public IEnumerable<NewTransactionItemDTO>? TransactionItemDTOs { get; set; }

    public static explicit operator Transaction(NewTransactionDTO newTransactionDTO)
    {
        return new Transaction
        {
            Guid = Guid.NewGuid(),
            EmployeeGuid = newTransactionDTO.EmployeeGuid,
            TransactionsDate = newTransactionDTO.TransactionDate,
            TotalAmmount = newTransactionDTO.TotalAmmount,
        };
    }
}
