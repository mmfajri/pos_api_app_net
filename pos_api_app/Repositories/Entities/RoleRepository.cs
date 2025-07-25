using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(PosDbContext posDbContext) : base(posDbContext) { }

    public Role? GetByName(string name)
    {
        return _posDbContext.Set<Role>().FirstOrDefault(role => role.Name.ToLower() == name.ToLower());
    }
}
