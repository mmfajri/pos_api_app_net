using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories;
using pos_api_app.Data;
using pos_api_app.Models;
using pos_api_app.Utilities;

namespace pos_api_app.Repository;

public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
    where TEntity : BaseEntity
{
	protected readonly PosDbContext _posDbContext;
	protected readonly ILogger<TEntity>? _logger;

	public GeneralRepository(PosDbContext posDbContext, ILogger<TEntity>? logger = null)
	{
		_posDbContext = posDbContext;
		_logger = logger is not null ? logger : null;
	}

	public async Task<IEnumerable<TEntity>> GetAll()
	{
		return await _posDbContext.Set<TEntity>().Where(e => e.IsDeleted != true).OrderByDescending(e => e.CreatedTime).ToListAsync();
	}

	public async Task<TEntity?> GetById(int id)
	{
		var entity = await _posDbContext.Set<TEntity>().Where(e => e.IsDeleted != true && e.Id == id).FirstOrDefaultAsync();
		_posDbContext.ChangeTracker.Clear();
		return entity;
	}

	public async Task<TEntity?> Create(TEntity entity)
	{
		try
		{
			await _posDbContext.Set<TEntity>().AddAsync(entity);
			await _posDbContext.SaveChangesAsync();
			return entity;
		}
		catch
		{
			return null;
		}
	}
	public async Task<bool> CreateBulk(List<TEntity> entities)
	{
		try
		{
			foreach (var item in entities)
			{
				await _posDbContext.Set<TEntity>().AddAsync(item);
			}
			await _posDbContext.SaveChangesAsync();
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> Update(TEntity entity)
	{
		try
		{
			_posDbContext.Set<TEntity>().Update(entity);
			await _posDbContext.SaveChangesAsync();
			return true;
		}
		catch
		{
			return false;
		}
	}
	public async Task<bool> Delete(TEntity entity)
	{
		try
		{
			_posDbContext.Set<TEntity>().Remove(entity);
			await _posDbContext.SaveChangesAsync();
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> IsExits(int id)
	{
		return await GetById(id) != null;
	}

}
