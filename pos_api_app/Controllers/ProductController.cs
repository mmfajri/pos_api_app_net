using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.CodeAnalysis;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class ProductController : ControllerBase
{
	private readonly ProductService _productService;

	public ProductController(ProductService productService)
	{
		_productService = productService;
	}

	[HttpGet]
	public async Task<IActionResult> Get([FromQuery] ProductTableDTO req)
	{
		var response = await _productService.Get(req);
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
					return StatusCode(StatusCodes.Status500InternalServerError, response);
				}
		}
	}

	[HttpGet("GetAllProductDropdown")]
	public async Task<IActionResult> GetAllProductDropdown()
	{
		var response = await _productService.GetAllProductPriceDropdown();
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
					return StatusCode(StatusCodes.Status500InternalServerError, response);
				}
		}
	}

	[HttpGet("GetProductByBarcodeIdDropdown")]
	public async Task<IActionResult> GetProductByBarcodeIdDropdown([FromQuery] string barcodeId)
	{
		var response = await _productService.GetProductByBarcodeDropdown(barcodeId);
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
					return StatusCode(StatusCodes.Status500InternalServerError, response);
				}
		}
	}

	[HttpPost("Create")]
	public async Task<IActionResult> Create(NewProductDTO productDTO)
	{
		var response = await _productService.Create(productDTO);
		switch (response.StatusCode)
		{
			case StatusCodes.Status400BadRequest:
				{
					return BadRequest(response);
				}
			case StatusCodes.Status403Forbidden:
				{
					return Unauthorized(response);
				}
			default:
				{
					return Ok(response);
				}
		}
	}

	[HttpPut("Update")]
	public async Task<IActionResult> Update(GetProductDTO productDTO)
	{
		var response = await _productService.Edit(productDTO);
		switch (response.StatusCode)
		{
			case StatusCodes.Status400BadRequest:
				{
					return BadRequest(response);
				}
			case StatusCodes.Status403Forbidden:
				{
					return Unauthorized(response);
				}
			case StatusCodes.Status404NotFound:
				{
					return NotFound(response);
				}
			default:
				{
					return Ok(response);
				}
		}
	}

	[HttpDelete("Delete")]
	public async Task<IActionResult> Delete(int id)
	{
		var response = await _productService.DeleteDataProductPrice(id);
		switch (response.StatusCode)
		{
			case StatusCodes.Status400BadRequest:
				{
					return BadRequest(response);
				}
			case StatusCodes.Status403Forbidden:
				{
					return Unauthorized(response);
				}
			case StatusCodes.Status404NotFound:
				{
					return NotFound(response);
				}
			default:
				{
					return Ok(response);
				}
		}
	}
}
