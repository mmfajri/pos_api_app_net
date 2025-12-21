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
	public string UnitItemList { get; set; } = string.Empty;

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
			UnitList = model.UnitItemList,
		};
	}
}

public class CreateInvoiceTransaction
{
	public string DateTimeTransaction { get; set; } = string.Empty;
	public string PosAccount { get; set; } = string.Empty;
	public decimal? TotalTransaction { get; set; }
	public List<CreateInvoiceTransactionItem>? TransactionItemList { get; set; }

	public static explicit operator Transaction(CreateInvoiceTransaction model)
	{
		return new Transaction
		{
			TransactionsDate = DateTime.Parse(model.DateTimeTransaction),
			AccountId = int.Parse(model.PosAccount),
			TotalAmmount = model.TotalTransaction,
		};
	}
}
