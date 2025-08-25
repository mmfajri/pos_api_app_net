namespace pos_api_app.Contracts.Repositories;

public interface IGeneralRepository<TEntity>
{
	Task<IEnumerable<TEntity>> GetAll();
	Task<TEntity?> GetById(int id);
	Task<TEntity?> Create(TEntity entity);
	Task<bool> Update(TEntity entity);
	Task<bool> Delete(TEntity entity);
	Task<bool> IsExits(int id);
}
