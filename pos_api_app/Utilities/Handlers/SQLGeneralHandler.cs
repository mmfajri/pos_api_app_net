namespace pos_api_app.Utilities.Handlers;

public class SQLGeneralHandler
{
	public static string PaginationHandler(string query, string sortColumn, string sortColumnDir, int rowPage, int pagePerRow)
	{
		int totalPagePerRows = rowPage * pagePerRow;
		string query = $@"SELECT * FROM ({query}) TBL ORDER BY TBL.{sortColumn} {sortColumnDir} LIMIT {totalPagePerRows} OFFSET {rowPage}";

		return query;
	}

}
