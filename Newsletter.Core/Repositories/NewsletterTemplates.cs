using Newsletter.Core.Repositories.Base;

namespace Newsletter.Core.Repositories
{
    public interface NewsletterTemplates : IRepositoryBase<Domain.NewsletterTemplate>
    {
        Task<bool> DoesNewsletterTemplateAlreadyExist(string templateName, Guid titelId);
    }
}
