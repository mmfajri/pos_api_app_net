using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IUnitRepository : IGeneralRepository<Unit>
{
	Task<Unit?> GetByName(string name);
	Task<bool> IsExits(string name);

}
