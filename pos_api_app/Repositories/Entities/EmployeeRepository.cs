using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Model.Entities;

namespace pos_api_app.Repository.Entities;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(PosDbContext posDbContext) : base(posDbContext) { }

    public bool IsUniqueUsername(string username)
    {
        return _posDbContext.Set<Employee>().Any(employee => employee.UserName == username);
    }

}
