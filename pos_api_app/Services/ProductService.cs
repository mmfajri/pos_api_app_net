using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.Models.Entities;
using pos_api_app.Utilities;

namespace pos_api_app.Services;

public class ProductService
{
	private readonly IProductRepository _productRepository;
	private readonly IPriceRepository _priceRepository;
	private readonly IUnitRepository _unitRepository;
	private readonly PosDbContext _posDbContext;

	public ProductService(IProductRepository productRepository, IPriceRepository priceRepository, IUnitRepository unitRepository, PosDbContext posDbContext)
	{
		_productRepository = productRepository;
		_priceRepository = priceRepository;
		_unitRepository = unitRepository;
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

	public async Task<ResponseDTO<bool>> Create(NewProductDTO req)
	{
		var response = new ResponseDTO<bool>();

		if (req == null)
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "Request cannot be null";
			return response;
		}
		// Add at the beginning of the method
		if (string.IsNullOrWhiteSpace(req.BarcodeID) || string.IsNullOrWhiteSpace(req.Unit))
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "BarcodeID and Unit are required";
			return response;
		}

		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				var price = (Price)req;
				//UNIT PROCESS
				var unitData = await _unitRepository.GetByName(req.Unit);
				if (unitData is null)
				{
					var unit = (Unit)req;
					unit.CreatedTime = DateTime.UtcNow;
					unit.IsDeleted = false;
					unitData = await _unitRepository.Create(unit);
					if (unitData is null)
					{

						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
					price.UnitId = unitData.Id;
				}
				else
				{
					price.UnitId = unitData.Id;
				}
				//PRODUCT PROCESS
				var productData = await _productRepository.GetByBarcode(req.BarcodeID);
				if (productData is null)
				{
					var product = (Product)req;
					product.CreatedTime = DateTime.UtcNow;
					product.IsDeleted = false;
					productData = await _productRepository.Create(product);
					if (productData is null)
					{
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
					price.ProductId = productData.Id;
				}
				else
				{
					price.ProductId = productData.Id;
				}
				//PRICE PROCESS
				var priceData = await _priceRepository.Create(price);
				if (priceData is null)
				{
					response.StatusCode = StatusCodes.Status400BadRequest;
					response.Message = StaticValue.ResponseMessage.ErrorSystem;
					return response;
				}

				await transactions.CommitAsync();

				response.StatusCode = StatusCodes.Status200OK;
				response.Message = StaticValue.ResponseMessage.Success;
				return response;

			}
			catch
			{
				await transactions.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
			}
		}
	}
}
