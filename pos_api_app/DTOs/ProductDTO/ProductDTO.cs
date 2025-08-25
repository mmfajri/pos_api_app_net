using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.ProductDTO;

public class ProductDTO
{
    public int Id { get; set; }
    public string BarcodeId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;


    public static explicit operator ProductDTO(Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            BarcodeId = product.BarcodeID,
            Title = product.Title,
        };
    }

    public static explicit operator Product(ProductDTO productDTO)
    {
        return new Product
        {
            BarcodeID = productDTO.BarcodeId,
            Title = productDTO.Title,
        };
    }
}
