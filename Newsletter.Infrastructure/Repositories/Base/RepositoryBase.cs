using Microsoft.EntityFrameworkCore;
using Newsletter.Core.Repositories.Base;
using Newsletter.Infrastructure.Persistence;

namespace Newsletter.Infrastructure.Repositories.Base;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
{
    protected readonly NewsletterDatastore _dbContext;

    public RepositoryBase(NewsletterDatastore dbContext)
    {
        this._dbContext = dbContext;
    }

    public Task<List<TEntity>> GetAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task Create(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Create(IEnumerable<TEntity> entity)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entity);
    }
}
