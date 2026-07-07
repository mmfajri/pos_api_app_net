using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using pos_api_app.DTOs.AuthDTO;
using pos_api_app.Services;
using pos_api_app.DTOs.UnitDTO;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class UnitController : ControllerBase
{
	private readonly UnitService _unitService;

	public UnitController(UnitService unitService)
	{
		_unitService = unitService;
	}

	[HttpGet("GetAllUnitDropdown")]
	public async Task<IActionResult> GetAllUnit()
	{
		var response = await _unitService.GetAllUnitDropdown();

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest(response);

		return Ok(response);
	}

	[HttpGet("GetUnitByNameDropdown")]
	public async Task<IActionResult> GetUnitByName([FromQuery] string name)
	{
		var response = await _unitService.GetUnitByNameDropdown(name);

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest(response);

		return Ok(response);
	}
}




