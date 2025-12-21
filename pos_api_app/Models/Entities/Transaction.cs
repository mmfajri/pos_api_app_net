using pos_api_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos_api_app.Models.Entities;

[Table("tb_tr_transaction")]
public class Transaction : BaseEntity
{
	[Column("account_id")]
	public int? AccountId { get; set; }

	[Column("transaction_date")]
	public DateTime TransactionsDate { get; set; }

	[Column("total_price_amount", TypeName = "decimal(18,2)")]
	public decimal? TotalAmmount { get; set; }

	[Column("pay_amount", TypeName = "decimal(18,2)")]
	public decimal? PayAmount { get; set; }

	//Cardinality
	public ICollection<TransactionItem>? TransactionItems { get; set; }
	public Account? Account { get; set; }

	public ICollection<Transaction>? Transactions { get; set; }
}
