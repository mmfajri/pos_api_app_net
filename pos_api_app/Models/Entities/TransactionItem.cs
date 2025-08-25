using pos_api_app.Models;
using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_transaction_item")]
public class TransactionItem : BaseEntity
{
    [Column("transaction_id")]
    public int? TransactionId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("price_id")]
    public int? PriceId { get; set; }

    [Column("quantity")]
    public float Quantity { get; set; }

    [Column("subtotal", TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }

    //Cardinality
    public Transaction? Transaction { get; set; }
    public Product? Product { get; set; }
    public Price? Price { get; set; }
}
