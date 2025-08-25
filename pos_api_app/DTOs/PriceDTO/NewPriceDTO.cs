using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.PriceDTO;

public class NewPriceDTO
{
    public int? ProductId { get; set; }
    public String UnitName { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public static explicit operator Price(NewPriceDTO newPriceDTO)
    {
        return new Price
        {
            ProductId = newPriceDTO.ProductId,
            Amount = newPriceDTO.Amount
        };
    }

}
