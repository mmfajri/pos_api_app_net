
using pos_api_app.DTOs.GeneralDTO;

namespace pos_api_app.DTOs.ProductDTO;

public class ProductTableDTO : TableDTO
{
	public string BarcodeId { get; set; } = string.Empty;
}
