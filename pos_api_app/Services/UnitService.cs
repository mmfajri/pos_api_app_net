using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ResponseDTO;
using pos_api_app.DTOs.UnitDTO;
using pos_api_app.Repository.Entities;
using pos_api_app.Utilities;
using pos_api_app.Models;
using pos_api_app.Models.Entities;
using pos_api_app.Data;

namespace pos_api_app.Services;

public class UnitService
{
	private readonly IUnitRepository _unitRepository;
	private readonly PosDbContext _posDbContext;

	public UnitService(IUnitRepository unitRepository, PosDbContext posDbContext)
	{
		_unitRepository = unitRepository;
		_posDbContext = posDbContext;
	}

	public async Task<ResponseDTO<List<UnitDTO>>> GetAllUnitDropdown()
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

	public async Task<ResponseDTO<UnitDTO>> GetUnitByNameDropdown(string req)
	{
		var response = new ResponseDTO<UnitDTO>();
		var dataDT0 = new UnitDTO();
		using (var transactions = await _posDbContext.Database.BeginTransactionAsync())
		{
			try
			{
				if (req != null)
				{
					var data = await _unitRepository.GetByName(req);
					if (data is null)
					{
						response.StatusCode = StatusCodes.Status404NotFound;
						response.Message = StaticValue.ResponseMessage.DataNotFound;
						return response;
					}
					else
					{
						dataDT0 = data;
					}
				}
				await transactions.CommitAsync();
				response.StatusCode = StatusCodes.Status200OK;
				response.Message = StaticValue.ResponseMessage.Success;
				response.Data = dataDT0;
				return response;
			}
			catch
			{
				await transactions.RollbackAsync();
				response.StatusCode = StatusCodes.Status400BadRequest;
				response.Message = StaticValue.ResponseMessage.ErrorSystem;
				return response;
			}

		}
	}
}



