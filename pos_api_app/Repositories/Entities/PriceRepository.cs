using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class PriceRepository : GeneralRepository<Price>, IPriceRepository
{
	public PriceRepository(PosDbContext posDbContext) : base(posDbContext) { }

	public async Task<bool> IsPricesExistByProductAndUnitID(decimal idProduct, decimal idUnit)
	{
		return await _posDbContext.Prices.AnyAsync(x => x.ProductId == idProduct && x.UnitId == idUnit);
	}

}
