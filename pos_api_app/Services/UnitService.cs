using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.UnitDTO;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities;

namespace pos_api_app.Services;

public class UnitService
{
	private readonly IUnitRepository _unitRepository;

	public UnitService(UnitRepository unitRepository)
	{
		_unitRepository = unitRepository;
	}

	public async Task<ResponseDTO<List<UnitDTO>>> GetAllUnit()
	{
		var response = new ResponseDTO<List<UnitDTO>>();
		var dataDto = new List<UnitDTO>();
		try
		{
			var data = await _unitRepository.GetAll();
			if (data is not null)
			{
				foreach (var item in data)
				{
					var entity = (UnitDTO)item;
					dataDto.Add(entity);
				}
			}
			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = dataDto;
			return response;
		}
		catch (Exception ex)
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
	}
}



