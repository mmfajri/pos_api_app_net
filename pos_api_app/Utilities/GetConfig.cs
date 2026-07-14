using Microsoft.Extensions.Configuration;

namespace pos_api_app.Utilities;

public class GetConfig
{
	public static IConfiguration AppSetting { get; }
	static GetConfig()
	{
		AppSetting = new ConfigurationBuilder()
		    .SetBasePath(Directory.GetCurrentDirectory())
		    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
		    .AddEnvironmentVariables()
		    .Build();
	}
}
