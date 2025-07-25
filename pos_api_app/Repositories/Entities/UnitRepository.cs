using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class UnitRepository : GeneralRepository<Unit>, IUnitRepository
{
    public UnitRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
    //Get Unit by Name
    public Unit? GetByName(string name)
    {
        return _posDbContext.Set<Unit>().FirstOrDefault(unit => unit.Name.ToLower() == name.ToLower());
    }

    public bool IsExits(string name)
    {
        return _posDbContext.Set<Unit>().Any(unit => unit.Name.ToLower() == name.ToLower());
    }
}
