using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.PriceDTO;

public class PriceDTO
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public int? UnitId { get; set; }
    public decimal Amount { get; set; }

    public static explicit operator PriceDTO(Price price)
    {
        return new PriceDTO
        {
            Id = price.Id,
            ProductId = price.ProductId,
            UnitId = price.UnitId,
            Amount = price.Amount,
        };
    }

    public static explicit operator Price(PriceDTO price)
    {
        return new Price
        {
            Amount = price.Amount
        };
    }
}
