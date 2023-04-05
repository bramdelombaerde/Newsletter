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
    }
}
