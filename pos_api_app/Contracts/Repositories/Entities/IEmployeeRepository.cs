using pos_api_app.Contracts.Repositories;
using pos_api_app.Model.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    public bool IsUniqueUsername(string username);
}
