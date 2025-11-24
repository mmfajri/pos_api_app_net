using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class UnitRepository : GeneralRepository<Unit>, IUnitRepository
{
	public UnitRepository(PosDbContext posDbContext) : base(posDbContext)
	{
	}
	//Get Unit by Name
	public async Task<Unit?> GetByName(string name)
	{
		return await _posDbContext.Set<Unit>().FirstOrDefaultAsync(unit => unit.Name.ToLower() == name.ToLower());
	}

	public async Task<bool> IsExits(string name)
	{
		return await _posDbContext.Set<Unit>().AnyAsync(unit => unit.Name.ToLower() == name.ToLower());
	}
}
