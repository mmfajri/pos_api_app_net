using pos_api_app.Model.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class NewProductDTO
{
    public string BarcodeID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public static explicit operator Product(NewProductDTO product)
    {
        return new Product
        {
            Guid = Guid.NewGuid(),
            BarcodeID = product.BarcodeID,
            Title = product.Title,
        };
    }
}
