using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_tr_price")]
public class Price : BaseEntity
{
    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("unit_id")]
    public int? UnitId { get; set; }

    [Column("amount", TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    //Cardinality
    public Product? Product { get; set; }
    public Unit? Unit { get; set; }
    public ICollection<TransactionItem>? TransactionItems { get; set; }
}
