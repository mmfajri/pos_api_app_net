using Microsoft.AspNetCore.Mvc;
using pos_api_app.DTOs.InvoiceDTO;
using pos_api_app.Services;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class InvoiceController : ControllerBase
{
	private readonly InvoiceService _invoiceService;

	public InvoiceController(InvoiceService invoiceService)
	{
		_invoiceService = invoiceService;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> SaveInvoiceTransaction(CreateInvoiceTransaction req)
	{
		var response = await _invoiceService.SaveInvoiceTransaction(req);
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


	[HttpGet("[action]")]
	public async Task<IActionResult> GetProductPriceByBarcode([FromQuery] InvoiceGetProductPriceDTO req)
	{
		var response = await _invoiceService.GetProductPriceByBarcodeId(req);
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


}
