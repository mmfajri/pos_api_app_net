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
	public async Task<IActionResult> Get([FromQuery] string? barcodeID = "")
	{
		var response = await _productService.Get(barcodeID);
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
	public async Task<IActionResult> Update(ProductDTO productDTO)
	{
		var isUpdated = await _productService.Edit(productDTO);
		switch (isUpdated)
		{
			case 0:
				return BadRequest(new ResponseHandler<ProductDTO>
				{
					Code = StatusCodes.Status400BadRequest,
					Status = HttpStatusCode.BadRequest.ToString(),
					Message = "Bad Connections, Data Failed to Update"
				});
			case -1:
				return NotFound(new ResponseHandler<ProductDTO>
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data that want to update is not found"
				});
			case 1:
				return Ok(new ResponseHandler<ProductDTO>
				{
					Code = StatusCodes.Status200OK,
					Status = HttpStatusCode.OK.ToString(),
					Message = "Successfully Updated",
				});
			default:
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ProductDTO>
				{
					Code = StatusCodes.Status500InternalServerError,
					Status = HttpStatusCode.InternalServerError.ToString(),
					Message = "Internal Server Error"
				});
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
