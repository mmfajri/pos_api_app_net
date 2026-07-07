using pos_api_app.DTOs.TransactionsDTO;

namespace pos_api_app.DTOs.GeneralDTO;

public class TableDTO
{
	public string SortColumn { get; set; } = string.Empty;
	public string SortColumnDir { get; set; } = string.Empty;
	public int RowsPerPage { get; set; } = 0;
	public int PageNumber { get; set; } = 0;
}

public class ResponseTableDTO<T>
{
	public int TotalRecord { get; set; } = 0;
	public int CurrentPage { get; set; } = 0;
	public int TotalPage { get; set; } = 0;
	public List<T>? DataTable { get; set; }
}
