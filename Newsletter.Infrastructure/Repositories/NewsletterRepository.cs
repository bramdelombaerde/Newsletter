using Microsoft.EntityFrameworkCore;
using Newsletter.Core.Repositories;
using Newsletter.Infrastructure.Persistence;
using Newsletter.Infrastructure.Repositories.Base;

namespace Newsletter.Infrastructure.Repositories
{
    public class NewsletterRepository : RepositoryBase<Domain.Newsletter>, Newsletters
    {
        public NewsletterRepository(NewsletterDatastore dbContext) : base(dbContext)
        {

        }

        public async Task<int> GetNextVersionNumberForTitel(Guid titelId)
        {
            var highestVersion = await _dbContext
                .Newsletters
                .Where(x => x.TitelId == titelId)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();

            if (highestVersion == null)
                return 1;

            return highestVersion.Version + 1;

        }

        public async Task<Domain.Newsletter> GetById(Guid id)
        {
            return await _dbContext
                .Newsletters
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
