using pos_api_app.DTOs.ProductDTO;

namespace pos_api_app.DTOs.InvoiceDTO;

public class InvoiceProductDTO : GetProductDTO
{
	public List<InvoiceUnitDTO>? UnitList { get; set; }

	public InvoiceProductDTO() { }

	public InvoiceProductDTO(GetProductDTO model)
	{
		PriceId = model.PriceId;
		BarcodeId = model.BarcodeId;
		Title = model.Title;
		QuantityType = model.QuantityType;
		Amount = model.Amount;
	}
}

