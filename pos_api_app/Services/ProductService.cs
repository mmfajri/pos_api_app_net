using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly PosDbContext _posDbContext;

    public ProductService(IProductRepository productRepository, PosDbContext posDbContext)
    {
        _productRepository = productRepository;
        _posDbContext = posDbContext;
    }

    public async Task<IEnumerable<ProductDTO>?> Get()
    {
        var list = await _productRepository.GetAll();
        if (list == null || !list.Any()) return null;

        var dto = list.Select(product => (ProductDTO)product);
        return dto;
    }

    public async Task<ProductDTO?> Get(string barcodeID)
    {
        var product = await _productRepository.GetByBarcode(barcodeID);
        if (product == null) return null;

        var dto = (ProductDTO)product;
        return dto;
    }

    public async Task<ProductDTO?> Get(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null) return null;

        var dto = (ProductDTO)product;
        return dto;
    }

    public async Task<int> Delete(int id)
    {
        var getEntity = await _productRepository.GetById(id);
        if (getEntity == null) return -1;

        var delete = await _productRepository.Delete(getEntity);
        if (!delete) return 0;

        return 1;
    }

    public async Task<int> Edit(ProductDTO product)
    {
        using (var transaction = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var IsExits = await _productRepository.IsExits(product.Id);
                if (!IsExits) return -1;

                var edit = await _productRepository.Update((Product)product);
                if (!edit) return 0;

                transaction.Commit();
                return 1;

            }
            catch
            {
                transaction.Rollback();
                return 0;
            }
        }
    }

    public async Task<int> Create(NewProductDTO newProductDTO)
    {
        using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var created = await _productRepository.Create((Product)newProductDTO);
                if (created == null) return 0;
                transactions.Commit();
                return 1;
            }
            catch
            {
                transactions.RollbackAsync();
                return 1;
            }
        }
    }
}
