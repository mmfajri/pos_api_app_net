using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.InvoiceDTO;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities;

namespace pos_api_app.Services;


public class InvoiceService
{
	private readonly PosDbContext _posDbContext;
	private readonly IProductRepository _productRepository;
	private readonly IUnitRepository _unitRepository;

	public InvoiceService(PosDbContext posDbContext, IProductRepository productRepository, IUnitRepository unitRepository)
	{
		_posDbContext = posDbContext;
		_productRepository = productRepository;
		_unitRepository = unitRepository;
	}

	public async Task<ResponseDTO<InvoiceProductDTO>> GetProductPriceByBarcodeId(InvoiceGetProductPriceDTO req)
	{
		var response = new ResponseDTO<InvoiceProductDTO>();
		var data = new ProductDTO();
		var unitList = new List<InvoiceUnitDTO>();
		using (var transaction = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				data = await _productRepository.GetSingleProductPriceByBarcodeId(req.BarcodeId, req.Unit);
				if (data is not null)
				{
					unitList = await _unitRepository.GetUnitByProductBarcodeId(req.BarcodeId);
				}
				var invoiceData = new InvoiceProductDTO(data!);
				invoiceData.UnitList = unitList;

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
