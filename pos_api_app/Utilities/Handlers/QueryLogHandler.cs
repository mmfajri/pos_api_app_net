using Dapper;

namespace pos_api_app.Utilities.Handlers;

public class QueryLogHandler
{
	public static void LogInfoQuery<T>(ILogger<T> logger, string query, DynamicParameters? parameter = null)
	{
		var paramInfo = parameter != null && parameter.ParameterNames.Any()
		    ? $"[{string.Join(", ", parameter.ParameterNames.Select(p => $"@{p}"))}]"
		    : "[]";

		logger.LogInformation("Executed DbCommand [Parameters={Params}, CommandType='Text', CommandTimeout='30']\n      {Query}",
		    paramInfo,
		    query.Replace("\n", "\n      "));
	}

}
