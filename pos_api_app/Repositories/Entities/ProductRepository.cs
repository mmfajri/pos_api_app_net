using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Models.Entities;

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

	public async Task<ProductDTO?> GetProduct(string? barcodeId = string.Empty)
	{
		var query = @"SELECT product.barcode_id BARCODE_ID, product.title TITLE, unit.Name UNIT,price.amount PRICE FROM tb_m_price price
			JOIN tb_m_product product on price.product_id = product.id
			JOIN tb_m_unit on price.unit_id = unit.id 
			WHERE product.isdeleted is null OR product.isdeleted = false";

		if (!string.IsNullOrEmpty(barcodeId))
		{
			query += "\n product.barcode_id = :barcodeId";
		}
	}
}
