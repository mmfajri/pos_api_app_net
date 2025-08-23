using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(PosDbContext posDbContext) : base(posDbContext) { }

    //public bool IsUniqueUsername(string username)
    //{
    //    return _posDbContext.Set<Employee>().Any(employee => employee.UserName == username);
    //}

}
