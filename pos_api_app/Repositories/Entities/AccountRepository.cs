using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
	public AccountRepository(PosDbContext posDbContext) : base(posDbContext) { }

	public async Task<bool> IsUniqueUsername(string username)
	{
		return await _posDbContext.Set<Account>().AnyAsync(employee => employee.UserName == username);
	}

	public async Task<Account> GetAccountByUsername(string username)
	{
		return await _posDbContext.Set<Account>().Where(account => account.UserName == username).FirstOrDefaultAsync();
	}

}
