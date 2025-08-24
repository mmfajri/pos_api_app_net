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
	public Task<IActionResult> Register(Regis)

}
