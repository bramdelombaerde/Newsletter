using Newsletter.Core.Repositories.Base;
using Newsletter.Domain;

namespace Newsletter.Core.Repositories
{
    public interface NewsletterTemplates : IRepositoryBase<Domain.NewsletterTemplate>
    {
        Task<bool> DoesNewsletterTemplateAlreadyExist(string templateName, Guid titelId);
        Task<NewsletterTemplate> GetById(Guid id);
    }
}
