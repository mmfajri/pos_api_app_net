using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.InvoiceDTO;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.Models.Entities;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities;

namespace pos_api_app.Services;


public class InvoiceService
{
	private readonly PosDbContext _posDbContext;
	private readonly ITransactionRepository _transactionRepository;
	private readonly ITransactionItemRepository _transactionItemRepository;
	private readonly IProductRepository _productRepository;
	private readonly IUnitRepository _unitRepository;

	public InvoiceService(PosDbContext posDbContext, IProductRepository productRepository, IUnitRepository unitRepository, ITransactionRepository transactionRepository, ITransactionItemRepository transactionItemRepository)
	{
		_posDbContext = posDbContext;
		_productRepository = productRepository;
		_unitRepository = unitRepository;
		_transactionRepository = transactionRepository;
		_transactionItemRepository = transactionItemRepository;
	}

	public async Task<ResponseDTO<bool>> SaveInvoiceTransaction(CreateInvoiceTransaction req)
	{
		var response = new ResponseDTO<bool>();
		using var transaction = await _posDbContext.Database.BeginTransactionAsync();
		try
		{
			//Save Transaction
			var transactionsData = (Transaction)req;
			transactionsData.CreatedTime = DateTime.UtcNow;
			transactionsData.IsDeleted = false;
			transactionsData = await _transactionRepository.Create(transactionsData);
			if (transactionsData is null)
			{
				await transaction.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
			}

			//Save Transaction Item
			if (req.ListTransactionItems is not null)
			{

				foreach (var item in req.ListTransactionItems)
				{
					var transactionItem = (TransactionItem)item;
					transactionItem.TransactionId = transactionsData.Id;
					transactionItem.CreatedTime = DateTime.UtcNow;
					transactionItem.IsDeleted = false;
					transactionItem = await _transactionItemRepository.Create(transactionItem);
					if (transactionItem is null)
					{

						await transaction.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
				}
			}
			else
			{
				response.StatusCode = StatusCodes.Status500InternalServerError;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
			}

			await transaction.CommitAsync();
			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = true;
			return response;

		}
		catch (Exception ex)
		{

			response.StatusCode = StatusCodes.Status500InternalServerError;
			response.Message = StaticValue.ResponseMessage.ErrorSystem + ex.Message + ex.InnerException;
			return response;
		}
	}

	public async Task<ResponseDTO<InvoiceProductDTO>> GetProductPriceByBarcodeId(InvoiceGetProductPriceDTO req)
	{
		var response = new ResponseDTO<InvoiceProductDTO>();
		var data = new GetProductDTO();
		var unitList = new List<InvoiceUnitDTO>();
		var invoiceData = new InvoiceProductDTO();
		using (var transaction = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				data = await _productRepository.GetSingleProductPriceByBarcodeId(req.BarcodeId, req.Unit);
				if (data is not null)
				{
					invoiceData = new InvoiceProductDTO(data!);
					unitList = await _unitRepository.GetUnitByProductBarcodeId(req.BarcodeId);
					if (unitList is not null)
					{
						invoiceData.UnitList = unitList;
					}
				}
				else
				{
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					return response;
				}

				response.StatusCode = StatusCodes.Status200OK;
				response.Message = StaticValue.ResponseMessage.Success;
				response.Data = invoiceData;
				return response;
			}
			catch (Exception ex)
			{
				response.StatusCode = StatusCodes.Status500InternalServerError;
				response.Message = StaticValue.ResponseMessage.ErrorSystem + ex.Message + ex.InnerException;
				return response;
			}
		}
	}
}
