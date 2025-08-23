using Microsoft.EntityFrameworkCore;
using pos_api_app.Contracts.Repositories;
using pos_api_app.Data;

namespace pos_api_app.Repository;

public class GeneralRepository<TEntity> /*: IGeneralRepository<TEntity>*/
    where TEntity : class
{
	protected readonly PosDbContext _posDbContext;

	public GeneralRepository(PosDbContext posDbContext)
	{
		_posDbContext = posDbContext;
	}

	public async Task<IEnumerable<TEntity>> GetAll()
	{
		return await _posDbContext.Set<TEntity>().ToListAsync();
	}

	public async Task<TEntity?> GetByGuid(Guid guid)
	{
		var entity = await _posDbContext.Set<TEntity>().FindAsync(guid);
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

	public bool IsExits(Guid guid)
	{
		return GetByGuid(guid) != null;
	}

}
