using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IProductRepository : IGeneralRepository<Product>
{
	Task<Product?> GetByBarcode(string barcode);
	Task<bool> UniqueBarcode(string barcode);
	Task<bool> IsProductExist(int id);

}
