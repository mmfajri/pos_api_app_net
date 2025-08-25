using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class TransactionItemRepository : GeneralRepository<TransactionItem>, ITransactionItemRepository
{
    public TransactionItemRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }

    public IEnumerable<TransactionItem>? GetByTransactionsId(int id)
    {
        return _posDbContext.Set<TransactionItem>().Where(x => x.TransactionId == id);
    }
}
