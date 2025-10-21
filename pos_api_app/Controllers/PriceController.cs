using pos_api_app.DTOs.PriceDTO;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]/")]
public class PriceController : ControllerBase
{
	private readonly PriceService _priceService;

	public PriceController(PriceService priceService)
	{
		_priceService = priceService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPrice()
	{
		var listPrice = await _priceService.GetAll();
		if (listPrice == null) return NotFound(new ResponseHandler<PriceDTO>
		{
			Code = StatusCodes.Status404NotFound,
			Status = HttpStatusCode.NotFound.ToString(),
			Message = "Data Not Found"
		});

		return Ok(new ResponseHandler<IEnumerable<PriceDTO>>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data Found",
			Data = listPrice
		});
	}
	[HttpGet("{id}")]
	public async Task<IActionResult> GetPrice(int id)
	{
		var price = await _priceService.GetPrice(id);
		if (price == null) return NotFound(new ResponseHandler<PriceDTO>
		{
			Code = StatusCodes.Status404NotFound,
			Status = HttpStatusCode.NotFound.ToString(),
			Message = "Data Not Found"
		});

		return Ok(new ResponseHandler<PriceDTO>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data Found",
			Data = price
		});
	}

	// [HttpPost("AddPrice/")]
	// public async Task<IActionResult> AddPrice(NewPriceDTO newPriceDTO)
	// {
	// 	var created = await _priceService.Create(newPriceDTO);
	// 	if (created != null)
	// 	{
	// 		return Ok(new ResponseHandler<PriceDTO>
	// 		{
	// 			Code = StatusCodes.Status200OK,
	// 			Status = HttpStatusCode.OK.ToString(),
	// 			Message = "Successfully Created",
	// 			Data = created
	// 		});
	// 	}
	// 	return NotFound(new ResponseHandler<ProductDTO>
	// 	{
	// 		Code = StatusCodes.Status404NotFound,
	// 		Status = HttpStatusCode.NotFound.ToString(),
	// 		Message = "Data Failed to created"
	// 	});
	// }

	[HttpPut("UpdatePrice/")]
	public async Task<IActionResult> UpdatePrice(PriceDTO price)
	{
		var updated = await _priceService.Edit(price);
		if (updated == false) return NotFound(new ResponseHandler<bool>
		{
			Code = StatusCodes.Status404NotFound,
			Status = HttpStatusCode.NotFound.ToString(),
			Message = "Data Failed to Updated",
			Data = updated
		});

		return Ok(new ResponseHandler<bool>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfully Updated",
			Data = updated
		});
	}

	[HttpDelete("DeletePrice/{id}")]
	public async Task<IActionResult> DeletePrice(int id)
	{
		var deleted = await _priceService.Delete(id);
		if (deleted == -1) return BadRequest(new ResponseHandler<PriceDTO>
		{
			Code = StatusCodes.Status400BadRequest,
			Status = HttpStatusCode.BadRequest.ToString(),
			Message = "Bad Connections, Data Failed to Delete"
		});
		if (deleted != 0) return NotFound(new ResponseHandler<PriceDTO>
		{
			Code = StatusCodes.Status404NotFound,
			Status = HttpStatusCode.NotFound.ToString(),
			Message = "Data that want to delete is not found"
		});

		return Ok(new ResponseHandler<int>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfully delete the data",
		});
	}
}
