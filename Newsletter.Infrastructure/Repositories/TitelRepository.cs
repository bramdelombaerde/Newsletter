using Microsoft.EntityFrameworkCore;
using Newsletter.Core.Repositories;
using Newsletter.Domain;
using Newsletter.Infrastructure.Persistence;
using Newsletter.Infrastructure.Repositories.Base;

namespace Newsletter.Infrastructure.Repositories
{
    public class TitelRepository : RepositoryBase<Titel>, Titels
    {
        public TitelRepository(NewsletterDatastore dbContext) : base(dbContext)
        {

        }

        public async Task<bool> DoesTitelAlreadyExist(string shortName)
        {
            return await _dbContext
                .Titels
                .AnyAsync(x =>
                    x.ShortName.Equals(shortName)
                );
        }
    }
}
