using pos_api_app.DTOs.ProductDTO;
using pos_api_app.DTOs.RolesDTO;
using pos_api_app.Services;
using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace pos_api_app.Controllers;

[ApiController]
[Route("pos_api_app/[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRole()
    {
        var listRole = await _roleService.GetAll();
        if (listRole == null)
        {
            return NotFound(new ResponseHandler<RoleDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<RoleDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listRole
        });
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(NewRoleDTO newRoleDTO)
    {
        int status = await _roleService.Create(newRoleDTO);
        switch (status)
        {
            case 0:
                return BadRequest(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Create"
                });
            case 1:
                return Ok(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Successfully create the data",
                });
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Unexpected status value"
                });
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = await _roleService.Delete(id);
        switch (delete)
        {
            case -1:
                return NotFound(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data that want to delete is not found"
                });
            case 0:
                return BadRequest(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Delete"
                });
            default: break;
        }
        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
            Data = delete
        });
    }
}
