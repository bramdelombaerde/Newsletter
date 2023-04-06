using Newsletter.Core.Repositories;
using Newsletter.Domain;
using Newsletter.Infrastructure.Persistence;
using Newsletter.Infrastructure.Repositories.Base;

namespace Newsletter.Infrastructure.Repositories
{
    public class NewsletterTemplateRepository : RepositoryBase<NewsletterTemplate>, NewsletterTemplates
    {
        public NewsletterTemplateRepository(NewsletterDatastore dbContext) : base(dbContext)
        {

        }
    }
}
