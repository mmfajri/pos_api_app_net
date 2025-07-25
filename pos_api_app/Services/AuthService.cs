using pos_api_app.Data;
using pos_api_app.DTOs;

namespace pos_api_app.Services;

public class AuthService
{
	private readonly PosDbContext _posDbContext;

	public AuthService(PosDbContext posDbContext)
	{
		_posDbContext = posDbContext;
	}

}
