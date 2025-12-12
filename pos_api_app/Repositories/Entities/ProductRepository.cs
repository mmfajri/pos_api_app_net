using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Models.Entities;
using pos_api_app.Utilities.Handlers;

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

	public async Task<(List<ProductDTO>?, int)> GetProduct(ProductTableDTO product)
	{
		var connection = _posDbContext.Database.GetDbConnection();

		var parameters = new DynamicParameters();
		var query = @"SELECT price.id id, product.barcode_id BarcodeId, product.title Title, unit.Name QuantityType, price.amount Amount 
                  FROM tb_tr_price price
                  JOIN tb_m_product product on price.product_id = product.id
                  JOIN tb_m_unit unit on price.unit_id = unit.id 
                  WHERE (product.is_deleted is null OR product.is_deleted  = false) and (price.is_deleted is null OR price.is_deleted = false)";

		if (!string.IsNullOrEmpty(product.BarcodeId))
		{
			query += "\n AND product.barcode_id = @barcodeId";
			parameters.Add("barcodeId", product.BarcodeId);
		}
		string queryCount = SQLGeneralHandler.CountData(query);
		query = SQLGeneralHandler.PaginationHandler(query, product.SortColumn, product.SortColumnDir, product.PageNumber, product.RowsPerPage);

		await connection.OpenAsync();
		var count = await connection.QueryAsync<int>(queryCount, parameters);
		var result = await connection.QueryAsync<ProductDTO>(query, parameters);
		return (result.ToList(), count.FirstOrDefault());
	}
}
