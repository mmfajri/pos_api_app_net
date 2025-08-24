using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using pos_api_app.DTOs.AuthDTO;
using pos_api_app.Services;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class AuthController : ControllerBase
{
	private readonly AuthService _authService;

	public AuthController(AuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register(RegisterDTO req)
	{
		var response = await _authService.RegisterUser(req);

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest(response);

		return Ok(response);

	}

}
