using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

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
    public async Task<IActionResult> Get()
    {
        var list = await _productService.Get();
        if (list == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandler<IEnumerable<ProductDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = list
        });
    }

    [HttpGet("Barcode/{barcodeID}")]
    public async Task<IActionResult> Get(string barcodeID)
    {
        var product = await _productService.Get(barcodeID);
        if (product == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data not found"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = product
        });
    }

    [HttpGet("{id}/")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.Get(id);
        if (product == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data not found"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = product
        });
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(NewProductDTO productDTO)
    {
        var created = await _productService.Create(productDTO);
        if (created == 0) return BadRequest(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Bad Connections, Data Failed to Add"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully Added",
        });
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
        var delete = await _productService.Delete(id);
        switch (delete)
        {
            case -1:
                return BadRequest(new ResponseHandler<ProductDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Delete"
                });
            case 0:
                return NotFound(new ResponseHandler<ProductDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data that want to delete is not found"
                });
        }
        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted the data",
        });
    }
}
