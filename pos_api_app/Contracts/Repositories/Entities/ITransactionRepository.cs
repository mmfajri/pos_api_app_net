using pos_api_app.Contracts.Repositories;
using pos_api_app.DTOs.TransactionsDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface ITransactionRepository : IGeneralRepository<Transaction>
{
	Task<(IEnumerable<TransactionDTO>, int)> GetListPaginated(GetTransactionDTO req);
}
