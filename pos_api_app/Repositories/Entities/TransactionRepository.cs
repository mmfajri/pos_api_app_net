using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.TransactionsDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class TransactionRepository : GeneralRepository<Transaction>, ITransactionRepository
{
	public TransactionRepository(PosDbContext posDbContext) : base(posDbContext)
	{
	}

	public async Task<(IEnumerable<TransactionDTO>, int)> GetListPaginated(GetTransactionDTO req)
	{
		// Start with base query
		var query = _posDbContext.Transactions.AsQueryable();

		// Apply date filter on entity level
		if (!string.IsNullOrEmpty(req.TransactionDate))
		{
			var dateFilter = DateTime.SpecifyKind(DateTime.Parse(req.TransactionDate), DateTimeKind.Utc).Date;
			query = query.Where(x => x.TransactionsDate.Date == dateFilter);
		}

		// Get total count
		var totalData = await query.CountAsync();

		// Apply sorting on entity level
		if (!string.IsNullOrEmpty(req.SortColumn))
		{
			query = req.SortColumnDir?.ToLower() == "desc"
			    ? query.OrderByDescending(GetEntitySortExpression(req.SortColumn))
			    : query.OrderBy(GetEntitySortExpression(req.SortColumn));
		}
		else
		{
			query = query.OrderByDescending(x => x.Id);
		}

		// Apply pagination and project to DTO
		var data = await query
		    .Skip((req.PageNumber - 1) * req.RowsPerPage)
		    .Take(req.RowsPerPage)
		    .Select(transaction => new TransactionDTO
		    {
			    TransactionDate = transaction.TransactionsDate,
			    TotalAmmount = transaction.TotalAmmount,
			    Id = transaction.Id
		    })
		    .ToListAsync();

		return (data, totalData);
	}

	private static Expression<Func<Transaction, object>> GetEntitySortExpression(string sortColumn)
	{
		return sortColumn?.ToLower() switch
		{
			"id" => x => x.Id,
			"transactiondate" => x => x.TransactionsDate,
			"totalammount" => x => x.TotalAmmount,
			_ => x => x.Id
		};
	}
}
