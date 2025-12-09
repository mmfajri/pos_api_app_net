namespace pos_api_app.DTOs.GeneralDTO;

public class TableDTO
{
	public string SortColumn { get; set; } = string.Empty;
	public string SortColumnDir { get; set; } = string.Empty;
	public int RowsPerPage { get; set; } = 0;
	public int PageNumber { get; set; } = 0;
}
