using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class NewProductDTO
{
    public string BarcodeID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public static explicit operator Product(NewProductDTO product)
    {
        return new Product
        {
            BarcodeID = product.BarcodeID,
            Title = product.Title,
        };
    }
}
