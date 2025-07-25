using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role? GetByName(string name);
}
