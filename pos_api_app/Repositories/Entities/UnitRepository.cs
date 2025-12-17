using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.InvoiceDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Repository.Entities;

public class UnitRepository : GeneralRepository<Unit>, IUnitRepository
{
	public UnitRepository(PosDbContext posDbContext) : base(posDbContext)
	{
	}
	public async Task<Unit?> GetByName(string name)
	{
		return await _posDbContext.Set<Unit>().FirstOrDefaultAsync(unit => unit.Name == name.ToLower());
	}

	public async Task<bool> IsExits(string name)
	{
		return await _posDbContext.Set<Unit>().AnyAsync(unit => unit.Name == name.ToLower());
	}

	public async Task<List<InvoiceUnitDTO>?> GetUnitByProductBarcodeId(string barcodeId)
	{
		var query = await (from price in _posDbContext.Prices
				   join product in _posDbContext.Products on price.ProductId equals product.Id into product_g
				   from product in product_g.DefaultIfEmpty()
				   join unit in _posDbContext.Units on price.UnitId equals unit.Id into unit_g
				   from unit in unit_g.DefaultIfEmpty()
				   where product.BarcodeID == barcodeId
				   orderby unit.Id ascending
				   select new InvoiceUnitDTO
				   {
					   Id = unit.Id,
					   Name = unit.Name
				   }).ToListAsync();
		return query;
	}
}
