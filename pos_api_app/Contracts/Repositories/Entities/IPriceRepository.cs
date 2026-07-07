using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IPriceRepository : IGeneralRepository<Price>
{
	Task<bool> IsPricesExistByProductAndUnitID(decimal idProduct, decimal idUnit);
}
