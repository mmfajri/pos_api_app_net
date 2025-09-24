using Microsoft.Extensions.Configuration;

namespace pos_api_app.Utilities;

public class GetConfig
{
	public static IConfiguration AppSetting { get; }
	static GetConfig()
	{
		AppSetting = new ConfigurationBuilder()
		    .SetBasePath(AppContext.BaseDirectory)
		    .AddJsonFile("appsettings.json")
		    .Build();
	}
}
