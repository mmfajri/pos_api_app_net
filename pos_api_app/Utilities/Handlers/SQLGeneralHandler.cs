namespace pos_api_app.Utilities.Handlers;

public class SQLGeneralHandler
{
	public static string PaginationHandler(string query, string sortColumn, string sortColumnDir, int rowPage, int pagePerRow)
	{
		// rowPage should be 1-based (page 1, page 2, etc.)
		int offset = (rowPage - 1) * pagePerRow;

		query = $@"SELECT * FROM ({query}) TBL ORDER BY TBL.{sortColumn} {sortColumnDir} LIMIT {pagePerRow} OFFSET {offset}";

		return query;
	}

	public static string CountData(string query)
	{
		query = $@"SELECT COUNT(1) FROM ({query})";
		return query;
	}
}
