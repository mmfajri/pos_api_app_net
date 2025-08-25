using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities
{
    public interface ITransactionItemRepository : IGeneralRepository<TransactionItem>
    {
        Task<IEnumerable<TransactionItem>?> GetByTransactionsId(int id);
    }
}
