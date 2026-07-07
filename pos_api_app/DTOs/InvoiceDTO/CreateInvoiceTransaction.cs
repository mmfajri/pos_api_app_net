using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.InvoiceDTO;

public class CreateInvoiceTransactionItem
{
	public string BarcodeId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string QuantityType { get; set; } = string.Empty;
	public int? Quantity { get; set; }
	public decimal? Price { get; set; }
	public decimal? TotalPrice { get; set; }

	public static explicit operator TransactionItem(CreateInvoiceTransactionItem model)
	{
		return new TransactionItem
		{
			BarcodeId = model.BarcodeId,
			TitleProduct = model.Title,
			QuantityType = model.QuantityType,
			Quantity = model.Quantity,
			PriceProduct = model.Price,
			TotalPrice = model.TotalPrice,
		};
	}
}

public class CreateInvoiceTransaction
{
	public string TransactionDate { get; set; } = string.Empty;
	public int? AccountPos { get; set; }
	public decimal? TotalTransaction { get; set; }
	public decimal? PayAmount { get; set; }
	public List<CreateInvoiceTransactionItem>? ListTransactionItems { get; set; }

	public static explicit operator Transaction(CreateInvoiceTransaction model)
	{
		return new Transaction
		{
			TransactionsDate = DateTime.SpecifyKind(DateTime.Parse(model.TransactionDate), DateTimeKind.Utc),
			AccountId = model.AccountPos is not null ? model.AccountPos : null,
			TotalAmmount = model.TotalTransaction,
			PayAmount = model.PayAmount,
		};
	}
}
