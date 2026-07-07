using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IAccountRepository : IGeneralRepository<Account>
{
	Task<bool> IsUniqueUsername(string username);
	Task<Account?> GetAccountByUsername(string username);
}
