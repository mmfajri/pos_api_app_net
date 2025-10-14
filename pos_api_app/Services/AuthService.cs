using pos_api_app.Data;
using pos_api_app.DTOs;
using pos_api_app.Models.Entities;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.AuthDTO;
using pos_api_app.Utilities;
using System.Net;

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
		var isUsernameExist = await _accountRepository.IsUniqueUsername(req.Username ?? string.Empty);
		if (isUsernameExist)
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "Invalid Credential Username";
			return response;
		}

		// Save it to the Database
		var model = (Account)req;
		model.IsDeleted = false;
		model.CreatedTime = DateTime.UtcNow;
		var isSuccess = await _accountRepository.Create(model);
		if (isSuccess is null)
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "Invalid Credential Username";
			return response;
		}

		response.StatusCode = StatusCodes.Status200OK;
		response.Message = StaticValue.ResponseMessage.Success;
		return response;
	}

	public async Task<ResponseDTO<AuthDTO>> Login(LoginDTO req)
	{
		var response = new ResponseDTO<AuthDTO>();

		//Validate Request
		if (string.IsNullOrEmpty(req.Username))
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "User Not Found";
			return response;
		}

		// Get Username
		var dataUser = await _accountRepository.GetAccountByUsername(req.Username);

		if (dataUser is null)
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = "User Not Found";
			return response;
		}

		if (dataUser.Password != req.Password)
		{
			response.StatusCode = StatusCodes.Status403Forbidden;
			response.Message = "Invalid Credential Username";
			return response;
		}

		// Generate Token JWT
		response.StatusCode = StatusCodes.Status200OK;
		response.Message = StaticValue.ResponseMessage.Success;
		return response;

	}
}
