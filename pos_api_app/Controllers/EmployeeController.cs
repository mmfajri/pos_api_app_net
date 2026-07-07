using pos_api_app.DTOs.EmployeesDTO;
using pos_api_app.DTOs.ProductDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]/")]
public class EmployeeController : ControllerBase
{
	private readonly EmployeeService _employeeService;

	public EmployeeController(EmployeeService employeeService)
	{
		_employeeService = employeeService;
	}

	[HttpGet]
	public async Task<IActionResult> GetEmployee()
	{
		var listEmployee = await _employeeService.Get();
		if (listEmployee == null) return NotFound(
		    new ResponseHandler<EmployeeDTO>
		    {
			    Code = StatusCodes.Status404NotFound,
			    Status = HttpStatusCode.NotFound.ToString(),
			    Message = "Data Not Found"
		    });

		return Ok(new ResponseHandler<IEnumerable<EmployeeDTO>>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data Found",
			Data = listEmployee,
		});
	}

	[HttpGet("{id}/")]
	public async Task<IActionResult> GetEmployee(int id)
	{
		var employee = await _employeeService.Get(id);
		if (employee == null) return NotFound(
		    new ResponseHandler<EmployeeDTO>
		    {
			    Code = StatusCodes.Status404NotFound,
			    Status = HttpStatusCode.NotFound.ToString(),
			    Message = "Data not found"
		    });
		return Ok(new ResponseHandler<EmployeeDTO>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Data was founded",
			Data = employee,
		});
	}

	[HttpPost("AddEmployee/")]
	public async Task<IActionResult> AddEmployee(NewEmployeeDTO newEmployeeDTO)
	{
		var created = await _employeeService.Create(newEmployeeDTO);
		if (created != null)
		{
			return Ok(new ResponseHandler<EmployeeDTO>
			{
				Code = StatusCodes.Status200OK,
				Status = HttpStatusCode.OK.ToString(),
				Message = "Successfully Added Data",
				Data = created,
			});
		}
		return BadRequest(new ResponseHandler<EmployeeDTO>
		{
			Code = StatusCodes.Status400BadRequest,
			Status = HttpStatusCode.BadRequest.ToString(),
			Message = "Data Failed to Add",
			Data = created
		});
	}

	[HttpPut("UpdatePrice/")]
	public async Task<IActionResult> UpdateEmployee(EmployeeDTO EmployeeDTO)
	{
		var update = await _employeeService.Edit(EmployeeDTO);
		if (!update)
		{
			return BadRequest(new ResponseHandler<EmployeeDTO>
			{
				Code = StatusCodes.Status400BadRequest,
				Status = HttpStatusCode.BadRequest.ToString(),
				Message = "Bad Connections, Data Failed to Update"
			});
		}
		return Ok(new ResponseHandler<EmployeeDTO>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "Successfully update the data",
			Data = EmployeeDTO
		});
	}

	[HttpDelete("Delete/{id}")]
	public async Task<IActionResult> DeleteEmployee(int id)
	{
		var delete = await _employeeService.Delete(id);
		switch (delete)
		{
			case -1:
				return NotFound(new ResponseHandler<EmployeeDTO>
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "The data that is to be deleted was not found."
				});
			case 0:
				return BadRequest(new ResponseHandler<EmployeeDTO>
				{
					Code = StatusCodes.Status400BadRequest,
					Status = HttpStatusCode.BadRequest.ToString(),
					Message = "Poor Connections: Data Deletion Failed"
				});
		}
		return Ok(new ResponseHandler<int>
		{
			Code = StatusCodes.Status200OK,
			Status = HttpStatusCode.OK.ToString(),
			Message = "The data was successfully deleted.",
			Data = delete
		});
	}
}
