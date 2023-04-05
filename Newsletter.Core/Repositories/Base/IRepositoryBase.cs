namespace Newsletter.Core.Repositories.Base;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    Task<List<TEntity>> GetAll();
    Task Create(TEntity entity);
    Task Create(IEnumerable<TEntity> entity);
    Task Update(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

