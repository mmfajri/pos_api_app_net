using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.PriceDTO;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.DTOs.UnitDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Services;

public class PriceService
{
	private readonly IPriceRepository _priceRepository;
	private readonly IProductRepository _productRepository;
	private readonly IUnitRepository _unitRepository;
	private readonly PosDbContext _posDbContext;

	public PriceService(IPriceRepository priceRepository, IProductRepository productRepository, PosDbContext posDbContext, IUnitRepository unitRepository)
	{
		_priceRepository = priceRepository;
		_productRepository = productRepository;
		_posDbContext = posDbContext;
		_unitRepository = unitRepository;
	}

	public async Task<IEnumerable<PriceDTO>?> GetAll()
	{
		var listPrice = await _priceRepository.GetAll();
		if (listPrice == null || !listPrice.Any()) return null;

		var dto = listPrice.Select(price => (PriceDTO)price);
		return dto;
	}

	public async Task<PriceDTO?> GetPrice(int id)
	{
		var price = await _priceRepository.GetById(id);
		if (price == null) return null;

		var dto = (PriceDTO)price;
		return dto;
	}

	// public async Task<PriceDTO?> Create(NewPriceDTO newPriceDTO)
	// {
	// 	using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
	// 	{
	// 		try
	// 		{
	// 			//check if the product is exist
	// 			var isProductExist = await _productRepository.IsExits((int)newPriceDTO.ProductId);
	// 			if (!isProductExist)
	// 			{
	// 				return null;
	// 			}
	//
	// 			//check existing unit price
	// 			var getUnit = _unitRepository.GetByName(newPriceDTO.UnitName);
	// 			Unit? newUnit = null;
	// 			if (getUnit == null)
	// 			{
	// 				newUnit = (Unit)new NewUnitDTO
	// 				{
	// 					Name = newPriceDTO.UnitName,
	// 				};
	// 				var createdUnit = _unitRepository.Create(newUnit);
	// 				if (createdUnit == null) return null;
	// 			}
	// 			var unitToUse = getUnit is not null ? getUnit : newUnit;
	//
	// 			var newPrice = (Price)newPriceDTO;
	// 			newPrice.UnitId = unitToUse!.Id;
	//
	// 			var createdPrice = await _priceRepository.Create(newPrice);
	// 			if (createdPrice == null) return null;
	// 			transactions.Commit();
	// 			return (PriceDTO)createdPrice;
	// 		}
	// 		catch
	// 		{
	// 			transactions.Rollback();
	// 			return null;
	// 		}
	// 	}
	// }

	public async Task<bool> Edit(PriceDTO priceDto)
	{
		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				//get price
				var getPrice = await _priceRepository.GetById(priceDto.Id);
				if (getPrice == null) return false;

				//set new price
				var newPrice = (Price)priceDto;
				newPrice.UnitId = getPrice.UnitId;
				newPrice.ProductId = getPrice.ProductId;


				//update the price
				var updatePrice = await _priceRepository.Update(newPrice);
				if (!updatePrice) return false;

				await transactions.CommitAsync();
				return true;
			}
			catch
			{
				await transactions.RollbackAsync();
				return false;
			}
		}
	}

	public async Task<int> Delete(int id)
	{
		using (var transaction = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				var getPrice = await _priceRepository.GetById(id);
				if (getPrice == null) return 0;

				var deletedPrice = await _priceRepository.Delete(getPrice);
				if (!deletedPrice) return 0;
				await transaction.CommitAsync();
				return 1;
			}
			catch
			{
				await transaction.RollbackAsync();
				return -1;
			}
		}

	}
}
