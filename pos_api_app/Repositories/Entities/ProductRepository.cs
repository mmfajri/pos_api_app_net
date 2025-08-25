using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.Models.Entities;
using pos_api_app.Models.Entities;
using System.Security.Cryptography.X509Certificates;

namespace pos_api_app.Repository.Entities;

public class ProductRepository : GeneralRepository<Product>, IProductRepository
{
    public ProductRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }

    public async Task<Product?> GetByBarcode(string barcode)
    {
        return await _posDbContext.Set<Product>().Where(product => product.BarcodeID == barcode).FirstOrDefaultAsync();
    }

    public async Task<bool> UniqueBarcode(string barcode)
    {
        return await _posDbContext.Set<Product>().AnyAsync(product => product.BarcodeID == barcode);
    }

    public async Task<bool> IsProductExist(int id)
    {
        return await _posDbContext.Set<Product>().AnyAsync(product => product.Id == id);
    }
}
