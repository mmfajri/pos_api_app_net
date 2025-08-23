using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IAccountRepository : IGeneralRepository<Account>
{
    //public bool IsUniqueUsername(string username);
}
