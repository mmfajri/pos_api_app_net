using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class TransactionItemRepository : GeneralRepository<TransactionItem>, ITransactionItemRepository
{
	public TransactionItemRepository(PosDbContext posDbContext) : base(posDbContext)
	{
	}

	public async Task<IEnumerable<TransactionItem>?> GetByTransactionsId(int id)
	{
		return await _posDbContext.Set<TransactionItem>().Where(x => x.TransactionId == id).ToListAsync();
	}
}
