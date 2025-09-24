using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class PriceRepository : GeneralRepository<Price>, IPriceRepository
{
	public PriceRepository(PosDbContext posDbContext) : base(posDbContext) { }

}
