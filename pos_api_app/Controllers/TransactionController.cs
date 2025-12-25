using pos_api_app.DTOs.TransactionsDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class TransactionController : ControllerBase
{
	private readonly TransactionService _transactionService;

	public TransactionController(TransactionService transactionService)
	{
		_transactionService = transactionService;
	}

	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] GetTransactionDTO req)
	{
		var response = await _transactionService.GetAll(req);
		switch (response.StatusCode)
		{
			case StatusCodes.Status400BadRequest:
				{
					return BadRequest(response);
				}
			case StatusCodes.Status404NotFound:
				{
					return NotFound(response);
				}
			case StatusCodes.Status200OK:
				{
					return Ok(response);
				}
			default:
				{
					return StatusCode(response.StatusCode, response);
				}
		}
	}
	// [HttpGet("{id}")]
	// public async Task<IActionResult> Get(int id)
	// {
	// 	var transaction = await _transactionService.GetDetailTransaction(id);
	// 	if (transaction == null)
	// 	{
	// 		return NotFound(new ResponseHandler<TransactionDTO>()
	// 		{
	// 			Code = StatusCodes.Status404NotFound,
	// 			Status = HttpStatusCode.NotFound.ToString(),
	// 			Message = "Data Not Found"
	// 		});
	// 	}
	// 	return Ok(new ResponseHandler<TransactionDTO>
	// 	{
	// 		Code = StatusCodes.Status200OK,
	// 		Status = HttpStatusCode.OK.ToString(),
	// 		Message = "Data Found",
	// 		Data = transaction
	// 	});
	// }
	//
	// [HttpPost("AddTransaction/")]
	// public async Task<IActionResult> AddTransaction(NewTransactionDTO transactionDTO)
	// {
	// 	var transaction = await _transactionService.Create(transactionDTO);
	// 	if (transaction == null)
	// 	{
	// 		return BadRequest(new ResponseHandler<TransactionDTO>()
	// 		{
	// 			Code = StatusCodes.Status400BadRequest,
	// 			Status = HttpStatusCode.BadRequest.ToString(),
	// 			Message = "Failed to Create Data"
	// 		});
	// 	}
	// 	return Ok(new ResponseHandler<TransactionDTO>
	// 	{
	// 		Code = StatusCodes.Status200OK,
	// 		Status = HttpStatusCode.OK.ToString(),
	// 		Message = "Data Successfully Created",
	// 		Data = transaction
	// 	});
	// }
	//
	// [HttpPut("UpdateTransaction/")]
	// public async Task<IActionResult> UpdateTransaction(TransactionDTO transactionDTO)
	// {
	// 	var isUpdated = await _transactionService.Edit(transactionDTO);
	// 	switch (isUpdated)
	// 	{
	// 		case (int)HttpStatusCode.BadRequest:
	// 			return BadRequest(new ResponseHandler<TransactionDTO>()
	// 			{
	// 				Code = StatusCodes.Status400BadRequest,
	// 				Status = HttpStatusCode.BadRequest.ToString(),
	// 				Message = "Data Failed to Updated"
	// 			});
	// 		case (int)HttpStatusCode.NotFound:
	// 			return NotFound(new ResponseHandler<TransactionDTO>()
	// 			{
	// 				Code = StatusCodes.Status404NotFound,
	// 				Status = HttpStatusCode.NotFound.ToString(),
	// 				Message = "Data Not Found"
	// 			});
	// 	}
	// 	return Ok(new ResponseHandler<TransactionDTO>
	// 	{
	// 		Code = StatusCodes.Status200OK,
	// 		Status = HttpStatusCode.OK.ToString(),
	// 		Message = "Data Successfully Edited"
	// 	});
	// }
	//
	// [HttpDelete("DeleteTransaction")]
	// public async Task<IActionResult> DeleteTransaction(int id)
	// {
	// 	var isDelete = await _transactionService.Delete(id);
	// 	switch (isDelete)
	// 	{
	// 		case (int)HttpStatusCode.BadRequest:
	// 			return BadRequest(new ResponseHandler<TransactionDTO>()
	// 			{
	// 				Code = StatusCodes.Status400BadRequest,
	// 				Status = HttpStatusCode.BadRequest.ToString(),
	// 				Message = "Data Failed to Delete"
	// 			});
	// 		case (int)HttpStatusCode.NotFound:
	// 			return NotFound(new ResponseHandler<TransactionDTO>()
	// 			{
	// 				Code = StatusCodes.Status404NotFound,
	// 				Status = HttpStatusCode.NotFound.ToString(),
	// 				Message = "Data Not Found"
	// 			});
	// 	}
	// 	return Ok(new ResponseHandler<TransactionDTO>
	// 	{
	// 		Code = StatusCodes.Status200OK,
	// 		Status = HttpStatusCode.OK.ToString(),
	// 		Message = "Data Successfully Delete"
	// 	});
	// }
}
