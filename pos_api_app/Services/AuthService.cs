using pos_api_app.Data;
using pos_api_app.DTOs;
using pos_api_app.Models.Entities;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.AuthDTO;

namespace pos_api_app.Services;

public class AuthService
{
	private readonly PosDbContext _posDbContext;
	private readonly IAccountRepository _accountRepository;

	public AuthService(PosDbContext posDbContext, IAccountRepository accountRepository)
	{
		_posDbContext = posDbContext;
		_accountRepository = accountRepository;
	}

	public async Task<ResponseDTO<bool>> RegisterUser(RegisterDTO req)
	{
		var response = new ResponseDTO<bool>();

		// Check if the Username Exist in the database


		// Save it to the Database

		return response;
	}

}
