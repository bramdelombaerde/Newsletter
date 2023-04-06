using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DoesNewsletterTemplateAlreadyExist(string templateName, Guid titelId)
        {
            return await _dbContext
                .NewsletterTemplates
                .Include(x => x.Titel)
                .AnyAsync(x =>
                    x.TemplateName.Equals(templateName) && x.Titel.Id == titelId
                );
        }
    }
}
