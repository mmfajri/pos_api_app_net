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
	public string ListUnitItem { get; set; } = string.Empty;

	public static explicit operator TransactionItem(CreateInvoiceTransactionItem model)
	{
		return new TransactionItem
		{
			BarcodeId = model.BarcodeId,
			TitleProduct = model.BarcodeId,
			QuantityType = model.QuantityType,
			Quantity = model.Quantity,
			PriceProduct = model.Price,
			TotalPrice = model.TotalPrice,
			UnitList = model.ListUnitItem,
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
			TransactionsDate = DateTime.Parse(model.TransactionDate),
			AccountId = model.AccountPos is not null ? model.AccountPos : null,
			TotalAmmount = model.TotalTransaction,
			PayAmount = model.PayAmount,
		};
	}
}
