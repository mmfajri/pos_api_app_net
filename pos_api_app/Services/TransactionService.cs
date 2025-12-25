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

	// public async Task<TransactionDTO?> GetDetailTransaction(int id)
	// {
	// 	var getTransaction = await _transactionRepository.GetById(id);
	// 	if (getTransaction == null) return null;
	// 	var transactionDto = (TransactionDTO)getTransaction;
	//
	// 	var TransactionItems = await _transactionItemRepository.GetByTransactionsId(transactionDto.Id);
	// 	if (TransactionItems != null)
	// 	{
	// 		foreach (var transactionItem in TransactionItems)
	// 		{
	// 			transactionDto.TransactionItemsDTO!.Add((TransactionItemDTO)transactionItem);
	// 		}
	// 	}
	// 	return transactionDto;
	// }

	// public async Task<TransactionDTO?> Create(NewTransactionDTO transactionDTO)
	// {
	// 	using (var transactionContext = _posDbContext.Database.BeginTransaction())
	// 	{
	// 		try
	// 		{
	// 			//check if the Employee is exist
	// 			var isExistEmployee = await _accountRepository.IsExits((int)transactionDTO.AccountId!);
	// 			if (!isExistEmployee)
	// 			{
	// 				return null;
	// 			}
	//
	// 			//insert the Transaction Detail to DB
	// 			var transaction = await _transactionRepository.Create((Transaction)transactionDTO);
	// 			if (transaction == null)
	// 			{
	// 				transactionContext.Rollback();
	// 				return null;
	// 			}
	//
	// 			//insert the List of Transaction Item to DB
	// 			foreach (var transactionItem in transactionDTO.TransactionItemDTOs!)
	// 			{
	// 				try
	// 				{
	// 					//checking the existing Price and Product 
	// 					if (!await _productRepository.IsExits((int)transactionItem.ProductId!))
	// 					{
	// 						return null;
	// 					}
	// 					if (!await _priceRepository.IsExits((int)transactionItem.PriceId!))
	// 					{
	// 						return null;
	// 					}
	//
	// 					var createdItem = (TransactionItem)transactionItem;
	// 					createdItem.TransactionId = transaction.Id;
	// 					var resultTransactionItem = await _transactionItemRepository.Create(createdItem);
	// 					if (resultTransactionItem == null)
	// 					{
	// 						transactionContext.Rollback();
	// 						return null;
	// 					}
	// 				    ;
	// 				}
	// 				catch
	// 				{
	//
	// 				}
	// 			}
	// 			transactionContext.Commit();
	// 			var dto = (TransactionDTO)transaction;
	// 			return dto;
	// 		}
	// 		catch
	// 		{
	// 			transactionContext.Rollback();
	// 			return null;
	// 		}
	// 	}
	// }
	// public async Task<int> Edit(TransactionDTO transactionDTO)
	// {
	// 	using (var transactionContext = _posDbContext.Database.BeginTransaction())
	// 	{
	// 		try
	// 		{
	// 			var isExist = await _transactionRepository.IsExits(transactionDTO.Id);
	// 			if (!isExist) return (int)HttpStatusCode.NotFound;
	//
	// 			//Edit The Transactions
	// 			var editedTransactions = await _transactionRepository.Update((Transaction)transactionDTO);
	// 			if (editedTransactions == false)
	// 			{
	// 				transactionContext.Rollback();
	// 				return (int)HttpStatusCode.BadRequest;
	// 			}
	//
	// 			//Edit the TransactionsItem
	// 			var getAllTransactionItems = await _transactionItemRepository.GetByTransactionsId(transactionDTO.Id);
	// 			if (getAllTransactionItems == null)
	// 			{
	// 				foreach (var transactionsItem in transactionDTO.TransactionItemsDTO!)
	// 				{
	// 					var createTransactionsItem = _transactionItemRepository.Create((TransactionItem)transactionsItem);
	// 					if (createTransactionsItem == null)
	// 					{
	// 						transactionContext.Rollback();
	// 						return (int)HttpStatusCode.BadRequest;
	// 					}
	// 				}
	// 			}
	// 			else
	// 			{
	// 				//Delete Existing Transactions Item
	// 				foreach (var transactionItem in getAllTransactionItems)
	// 				{
	// 					var deleteTransactionsItem = await _transactionItemRepository.Delete(transactionItem);
	// 					if (deleteTransactionsItem == false)
	// 					{
	// 						transactionContext.Rollback();
	// 						return (int)HttpStatusCode.BadRequest;
	// 					}
	// 				}
	// 				//Create New TransactionItem on the Transaction
	// 				foreach (var transactionItem in transactionDTO.TransactionItemsDTO!)
	// 				{
	// 					var createTransactionItem = _transactionItemRepository.Create((TransactionItem)transactionItem);
	// 					if (createTransactionItem == null)
	// 					{
	// 						transactionContext.Rollback();
	// 						return (int)HttpStatusCode.BadRequest;
	// 					}
	// 				}
	// 			}
	// 			transactionContext.Commit();
	// 			return (int)HttpStatusCode.OK;
	// 		}
	// 		catch
	// 		{
	// 			transactionContext.Rollback();
	// 			return (int)HttpStatusCode.BadRequest;
	// 		}
	// 	}
	// }

	public async Task<int> Delete(int id)
	{
		using (var transactionContext = _posDbContext.Database.BeginTransaction())
		{
			try
			{
				//Check Existing Transactions
				var isExist = await _transactionRepository.IsExits(id);
				if (isExist == false) return (int)HttpStatusCode.NotFound;
				var getTransaction = await _transactionRepository.GetById(id)!;

				//Delete TransactionsItem
				if (getTransaction is not null)
				{
					if (getTransaction.TransactionItems != null)
					{
						var getTransactionItem = await _transactionItemRepository.GetByTransactionsId(id)!;
						if (getTransactionItem is not null)
						{
							foreach (var transactionItem in getTransactionItem)
							{
								var deleteTransactionItem = await _transactionItemRepository.Delete(transactionItem);
								if (!deleteTransactionItem)
								{
									transactionContext.Rollback();
									return (int)HttpStatusCode.BadRequest;
								}
							}
						}
					}
					//Delete Transaction
					var deleteTransaction = await _transactionRepository.Delete(getTransaction);
					if (!deleteTransaction)
					{
						transactionContext.Rollback();
						return (int)HttpStatusCode.BadRequest;
					}
				}
				transactionContext.Commit();
				return (int)HttpStatusCode.OK;
			}
			catch
			{
				transactionContext.Rollback();
				return (int)HttpStatusCode.BadRequest;
			}
		}
	}
}
