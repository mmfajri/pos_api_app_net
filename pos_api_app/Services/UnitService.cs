using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.UnitDTO;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities;
using pos_api_app.Models;
using pos_api_app.Models.Entities;

namespace pos_api_app.Services;

public class UnitService
{
	private readonly IUnitRepository _unitRepository;

	public UnitService(IUnitRepository unitRepository)
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
			Console.WriteLine(ex);
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
	}

	public async Task<ResponseDTO<UnitDTO>> GetUnitByName(NewUnitDTO req)
	{
		var response = new ResponseDTO<UnitDTO>();
		var dataDT0 = new UnitDTO();

		try
		{
			var data = await _unitRepository.GetByName(req.Name);
			if (data is null)
			{
				data = (Unit)req;
				data.Name = data.Name.ToLower();
				data.CreatedTime = DateTime.Now;
				data.IsDeleted = false;

				var newData = await _unitRepository.Create(data);
				if (newData is not null) dataDT0 = newData;
			}
			else
			{
				dataDT0 = data;
			}
			response.StatusCode = StatusCodes.Status200OK;
			response.Message = StaticValue.ResponseMessage.Success;
			response.Data = dataDT0;
			return response;
		}
		catch
		{
			response.StatusCode = StatusCodes.Status400BadRequest;
			response.Message = StaticValue.ResponseMessage.ErrorSystem;
			return response;
		}
	}
}



