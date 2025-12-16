using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.GeneralDTO;
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

	public async Task<ResponseDTO<ResponseTableDTO<GetProductDTO>?>> Get(ProductTableDTO req)
	{
		var response = new ResponseDTO<ResponseTableDTO<GetProductDTO>?>();
		using var transaction = await _posDbContext.Database.BeginTransactionAsync();
		try
		{
			var (data, count) = await _productRepository.GetProduct(req);
			if (data is null || data.Count == 0)
			{
				response.StatusCode = StatusCodes.Status404NotFound;
				response.Message = StaticValue.ResponseMessage.DataNotFound;
				return response;
			}
			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = new ResponseTableDTO<GetProductDTO> // Initialize Data first
			{
				DataTable = data,
				TotalRecord = count,
				CurrentPage = req.PageNumber,
				TotalPage = (int)Math.Ceiling(count / (double)req.RowsPerPage)
			};
			return response;
		}
		catch
		{
			response.StatusCode = StatusCodes.Status500InternalServerError;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
	}

	public async Task<ResponseDTO<List<ProductDTODropdown>?>> GetAllProductPriceDropdown()
	{
		var response = new ResponseDTO<List<ProductDTODropdown>?>();
		var dataDropdown = new List<ProductDTODropdown>();

		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			var dataProduct = await _productRepository.GetAll();
			if (dataProduct is null)
			{
				response.StatusCode = StatusCodes.Status404NotFound;
				response.Message = StaticValue.ResponseMessage.DataNotFound;
				response.Data = null;
				return response;
			}
			foreach (var item in dataProduct)
			{
				var data = (ProductDTODropdown)item;
				dataDropdown.Add(data);
			}
		}
		response.StatusCode = StatusCodes.Status200OK;
		response.Message = StaticValue.ResponseMessage.Success;
		response.Data = dataDropdown;
		return response;
	}

	public async Task<ResponseDTO<ProductDTODropdown?>> GetProductByBarcodeDropdown(string BarcodeId)
	{
		var response = new ResponseDTO<ProductDTODropdown?>();
		var dataDropdown = new ProductDTODropdown();

		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			var dataProduct = await _productRepository.GetByBarcode(BarcodeId);
			if (dataProduct is null)
			{
				response.StatusCode = StatusCodes.Status404NotFound;
				response.Message = StaticValue.ResponseMessage.DataNotFound;
				response.Data = null;
				return response;
			}
			dataDropdown = (ProductDTODropdown)dataProduct;
			await transactions.CommitAsync();
		}
		response.StatusCode = StatusCodes.Status200OK;
		response.Message = StaticValue.ResponseMessage.Success;
		response.Data = dataDropdown;
		return response;
	}

	public async Task<ResponseDTO<int?>> DeleteDataProductPrice(int id)
	{
		var response = new ResponseDTO<int?>();
		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				var getPrice = await _priceRepository.GetById(id);
				if (getPrice is null)
				{
					await transactions.RollbackAsync();
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					return response;
				}
				getPrice.IsDeleted = true;
				var isSucced = await _priceRepository.Update(getPrice);
				if (!isSucced)
				{
					await transactions.RollbackAsync();
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					return response;
				}

				await transactions.CommitAsync();
				response.StatusCode = StatusCodes.Status200OK;
				response.Message = StaticValue.ResponseMessage.Success;
				return response;

			}
			catch (Exception ex)
			{
				await transactions.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem + ex.Message + ex.InnerException;
				return response;
			}
		}
	}

	public async Task<ResponseDTO<bool>> Edit(GetProductDTO req)
	{
		var response = new ResponseDTO<bool>();
		using (var transaction = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				// Get Product Id
				var productData = await _productRepository.GetByBarcode(req.BarcodeId);
				if (productData is null)
				{
					await transaction.RollbackAsync();
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					response.Data = false;
					return response;
				}
				productData.Title = req.Title;
				productData.BarcodeID = req.BarcodeId;
				productData.UpdatedTime = DateTime.UtcNow;
				var isUpdated = await _productRepository.Update(productData);
				if (!isUpdated)
				{
					await transaction.RollbackAsync();
					response.StatusCode = StatusCodes.Status400BadRequest;
					response.Message = StaticValue.ResponseMessage.ErrorSystem;
					response.Data = isUpdated;
					return response;
				}

				// Get Unit Id
				var unitData = await _unitRepository.GetByName(req.QuantityType);
				if (unitData is null)
				{
					var unit = (Unit)req;
					unit.CreatedTime = DateTime.UtcNow;
					unit.IsDeleted = false;
					unitData = await _unitRepository.Create(unit);
					if (unitData is null)
					{
						await transaction.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
				}
				// Update Price 
				var priceData = await _priceRepository.GetById(req.PriceId);
				if (priceData is null)
				{
					await transaction.RollbackAsync();
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					return response;
				}
				priceData.UnitId = unitData.Id;
				priceData.ProductId = priceData.Id;
				priceData.UpdatedTime = DateTime.UtcNow;
				priceData.Amount = req.Amount;
				var edit = await _priceRepository.Update(priceData);
				if (!edit)
				{
					await transaction.RollbackAsync();
					response.StatusCode = StatusCodes.Status400BadRequest;
					response.Message = StaticValue.ResponseMessage.ErrorSystem;
					return response;
				}
				await transaction.CommitAsync();
				return response;
			}
			catch
			{
				await transaction.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
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
		if (string.IsNullOrWhiteSpace(req.BarcodeID) || string.IsNullOrWhiteSpace(req.QuantityType))
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "BarcodeID and/or Unit are required";
			return response;
		}

		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				var price = (Price)req;
				//UNIT PROCESS
				var unitData = await _unitRepository.GetByName(req.QuantityType);
				if (unitData is null)
				{
					var unit = (Unit)req;
					unit.CreatedTime = DateTime.UtcNow;
					unit.IsDeleted = false;
					unitData = await _unitRepository.Create(unit);
					if (unitData is null)
					{
						await transactions.RollbackAsync();
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
						await transactions.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
					price.ProductId = productData.Id;
				}
				else
				{
					if (productData.Title.Equals(req.Title, StringComparison.OrdinalIgnoreCase))
					{
						await transactions.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
					price.ProductId = productData.Id;
				}
				//PRICE PROCESS
				price.CreatedTime = DateTime.UtcNow;
				price.IsDeleted = false;
				var priceData = await _priceRepository.Create(price);
				if (priceData is null)
				{
					await transactions.RollbackAsync();
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
