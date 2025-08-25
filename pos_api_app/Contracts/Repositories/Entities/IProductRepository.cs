using pos_api_app.Contracts.Repositories;
using pos_api_app.Models.Entities;

namespace pos_api_app.Contracts.Repositories.Entities;

public interface IProductRepository : IGeneralRepository<Product>
{
    Product? GetByBarcode(string barcode);
    bool UniqueBarcode(string barcode);
    bool IsProductExist(int id);

}
