using pos_api_app.Models;
using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_tr_transaction_item")]
public class TransactionItem : BaseEntity
{
	[Column("transaction_id")]
	public int? TransactionId { get; set; }

	[Column("barcode_id", TypeName = "varchar(255)")]
	public string? BarcodeId { get; set; }

	[Column("title_product", TypeName = "varchar(255)")]
	public string? TitleProduct { get; set; }

	[Column("quantity_type", TypeName = "varchar(200)")]
	public string? QuantityType { get; set; }

	[Column("quantity")]
	public decimal? Quantity { get; set; }

	[Column("price_product", TypeName = "decimal(18,2)")]
	public decimal? PriceProduct { get; set; }

	[Column("total_price_product", TypeName = "decimal(18,2)")]
	public decimal? TotalPrice { get; set; }

	// [Column("unit_List", TypeName = "varchar(255)")]
	// public string? UnitList { get; set; }

	//No Need for fastes performance
	//Cardinality
	// public Transaction? Transaction { get; set; }
}
