namespace pos_api_app.DTOs.InvoiceDTO;

public class InvoiceProductDTO : ProductDTO.ProductDTO
{
	public List<InvoiceUnitDTO>? UnitList { get; set; }

	public InvoiceProductDTO() { }

	public InvoiceProductDTO(ProductDTO.ProductDTO model)
	{
		Id = model.Id;
		BarcodeId = model.BarcodeId;
		Title = model.Title;
		QuantityType = model.QuantityType;
		Amount = model.Amount;
	}
}

