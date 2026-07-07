using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.GeneralDTO;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.TransactionsDTO;
using pos_api_app.DTOs.TransactionsItemDTO;
using pos_api_app.Models.Entities;
using pos_api_app.Utilities;
using System.Data;
using System.Net;

namespace pos_api_app.Services;

public class TransactionService
{
	private readonly ITransactionRepository _transactionRepository;
	private readonly ITransactionItemRepository _transactionItemRepository;
	private readonly IProductRepository _productRepository;
	private readonly IPriceRepository _priceRepository;
	private readonly IAccountRepository _accountRepository;
	private readonly PosDbContext _posDbContext;

	public TransactionService(ITransactionRepository transactionRepository,
	      ITransactionItemRepository transactionItemRepository,
	      IAccountRepository accountRepository,
	      IPriceRepository priceRepository,
	      IProductRepository productRepository,
	      PosDbContext posDbContext)
	{
		_transactionRepository = transactionRepository;
		_transactionItemRepository = transactionItemRepository;
		_accountRepository = accountRepository;
		_priceRepository = priceRepository;
		_productRepository = productRepository;
		_posDbContext = posDbContext;
	}
	// NOTE: INSERT TRANSACTION 
	public async Task<ResponseDTO<bool>> Create(NewTransactionDTO req)
	{
		var response = new ResponseDTO<bool>();

		using var trxDb = await _posDbContext.Database.BeginTransactionAsync();
		try
		{
			//Map the TransactionDTO to TransactionTable
			var transaction = (Transaction)req;
			var dataTransaction = await _transactionRepository.Create(transaction);
			if (dataTransaction is null)
			{
				await trxDb.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				response.Data = false;
				return response;
			}


			if (req.TransactionItemDTOs is not null)
			{
				//Mapping the TransactionItemDto to TransactionItemTable
				foreach (var item in req.TransactionItemDTOs)
				{
					var itemTransaction = (TransactionItem)item;
					itemTransaction.TransactionId = dataTransaction.Id;
					var dataItemTransaction = _transactionItemRepository.Create(itemTransaction);
					if (dataItemTransaction is null)
					{
						await trxDb.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						response.Data = false;
						return response;
					}
				}
			}
			await trxDb.CommitAsync();

		}
		catch (Exception ex)
		{
			await trxDb.RollbackAsync();
			response.StatusCode = StatusCodes.Status500InternalServerError;
			response.Message = StaticValue.ResponseMessage.ErrorSystem + $" {ex}";
			return response;
		}
		return response;
	}

	public async Task<ResponseDTO<ResponseTableDTO<TransactionItemDTO>>> GetDetailTransaction(long id)
	{
		var response = new ResponseDTO<ResponseTableDTO<TransactionItemDTO>>();
		var data = new ResponseTableDTO<TransactionItemDTO>();

		using var trxContext = await _posDbContext.Database.BeginTransactionAsync();
		try
		{
			var transactionItemList = await _transactionItemRepository.GetByTransactionsId((int)id);
			if (transactionItemList is null || transactionItemList.Count() == 0)
			{
				response.StatusCode = StatusCodes.Status404NotFound;
				response.Message = StaticValue.ResponseMessage.DataNotFound;
				return response;
			}

			foreach (var item in transactionItemList)
			{
				if (item is not null)
				{
					var itemDto = (TransactionItemDTO)item;
					data.DataTable!.Add(itemDto);
				}
			}
			data.CurrentPage = 1;
			data.TotalRecord = transactionItemList.Count();
			data.TotalPage = 1;

			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = data;
			return response;
		}
		catch
		{
			response.StatusCode = StatusCodes.Status500InternalServerError;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
	}

	public async Task<ResponseDTO<ResponseTableDTO<TransactionDTO>>> GetAll(GetTransactionDTO req)
	{
		var response = new ResponseDTO<ResponseTableDTO<TransactionDTO>>();
		var data = new ResponseTableDTO<TransactionDTO>();
		using var trxContext = await _posDbContext.Database.BeginTransactionAsync();

		var (transactions, count) = await _transactionRepository.GetListPaginated(req);
		if (transactions == null)
		{
			await trxContext.RollbackAsync();
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
		data.TotalRecord = count;
		data.DataTable = (List<TransactionDTO>?)transactions;
		data.CurrentPage = req.PageNumber;
		data.TotalPage = (int)Math.Ceiling(count / (double)req.RowsPerPage);


		await trxContext.CommitAsync();
		response.StatusCode = StatusCodes.Status200OK;
		response.Message = StaticValue.ResponseMessage.Success;
		response.Data = data;
		return response;
	}

	public async Task<ResponseDTO<bool>> UpdateTransactionItems(UpdateTransactionItemDTO req)
	{
		var response = new ResponseDTO<bool>();

		using var trxDb = await _posDbContext.Database.BeginTransactionAsync();
		try
		{
			// Check if transaction exists
			var isExist = await _transactionRepository.IsExits(req.TransactionId);
			if (!isExist)
			{
				response.StatusCode = StatusCodes.Status404NotFound;
				response.Message = StaticValue.ResponseMessage.DataNotFound;
				response.Data = false;
				return response;
			}

			// Force delete all existing transaction items for this transaction
			var deleteResult = await _transactionItemRepository.DeleteByTransactionId(req.TransactionId);
			if (!deleteResult)
			{
				await trxDb.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				response.Data = false;
				return response;
			}

			// Add new transaction items from request
			if (req.ListTransactionItem is not null)
			{
				foreach (var item in req.ListTransactionItem)
				{
					var transactionItem = (TransactionItem)item;
					transactionItem.TransactionId = req.TransactionId;
					var created = _transactionItemRepository.Create(transactionItem);
					if (created is null)
					{
						await trxDb.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						response.Data = false;
						return response;
					}
				}
			}

			await trxDb.CommitAsync();
			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = true;
		}
		catch (Exception ex)
		{
			await trxDb.RollbackAsync();
			response.StatusCode = StatusCodes.Status500InternalServerError;
			response.Message = StaticValue.ResponseMessage.ErrorSystem + $" {ex}";
			response.Data = false;
		}
		return response;
	}

	public async Task<ResponseDTO<bool>> Delete(int id)
	{

		var response = new ResponseDTO<bool>();
		using (var transactionContext = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				//Check Existing Transactions
				var isExist = await _transactionRepository.IsExits(id);
				if (isExist == false)
				{
					response.StatusCode = StatusCodes.Status404NotFound;
					response.Message = StaticValue.ResponseMessage.DataNotFound;
					return response;
				}
				var getTransaction = await _transactionRepository.GetById(id)!;

				//Delete TransactionsItem
				if (getTransaction is not null)
				{
					var getTransactionItem = await _transactionItemRepository.GetByTransactionsId(id);
					if (getTransactionItem is not null)
					{
						foreach (var item in getTransactionItem)
						{
							var deleteTransactionItem = await _transactionItemRepository.Delete(item);
							if (!deleteTransactionItem)
							{
								await transactionContext.RollbackAsync();
								response.StatusCode = StatusCodes.Status400BadRequest;
								response.Message = StaticValue.ResponseMessage.ErrorSystem;
								return response;
							}
						}
					}
					//Delete Transaction
					var deleteTransaction = await _transactionRepository.Delete(getTransaction);
					if (!deleteTransaction)
					{
						await transactionContext.RollbackAsync();
						response.StatusCode = StatusCodes.Status400BadRequest;
						response.Message = StaticValue.ResponseMessage.ErrorSystem;
						return response;
					}
				}
				await transactionContext.CommitAsync();
				response.StatusCode = StatusCodes.Status200OK;
				response.Message = StaticValue.ResponseMessage.Success;
				return response;
			}
			catch
			{
				await transactionContext.RollbackAsync();
				response.StatusCode = StatusCodes.Status500InternalServerError;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
			}
		}
	}
}
