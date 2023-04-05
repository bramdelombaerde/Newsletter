using Newsletter.Core.Repositories.Base;
using Newsletter.Domain;

namespace Newsletter.Core.Repositories
{
    public interface Users : IRepositoryBase<Domain.User>
    {
        Task<bool> DoesUserAlreadyExist(string email);
        Task<User> GetById(Guid id);
    }
}
