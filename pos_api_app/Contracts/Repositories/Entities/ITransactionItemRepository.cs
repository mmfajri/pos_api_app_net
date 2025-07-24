using pos_api_app.Contracts.Repositories;
using pos_api_app.Model.Entities;

namespace pos_api_app.Contracts.Repositories.Entities
{
    public interface ITransactionItemRepository : IGeneralRepository<TransactionItem>
    {
        IEnumerable<TransactionItem>? GetByTransactionsGuid(Guid guid);
    }
}
