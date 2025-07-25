using pos_api_app.Models;
using pos_api_app.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_m_product")]
public class Product : BaseEntity
{
	[Column("barcode_id", TypeName = "varchar(255)")]
	public string BarcodeID { get; set; } = string.Empty;

	[Column("title", TypeName = "varchar(150)")]
	public string Title { get; set; } = string.Empty;

	//Cardinality
	public ICollection<Price>? Prices { get; set; }
	public ICollection<TransactionItem>? TransactionItems { get; set; }
}
