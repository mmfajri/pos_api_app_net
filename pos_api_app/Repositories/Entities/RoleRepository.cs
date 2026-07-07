using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
	public RoleRepository(PosDbContext posDbContext) : base(posDbContext) { }

	public async Task<Role?> GetByName(string name)
	{
		return await _posDbContext.Set<Role>().FirstOrDefaultAsync(role => role.Name.ToLower() == name.ToLower());
	}
}
