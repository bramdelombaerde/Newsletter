using Newsletter.Core.Repositories.Base;

namespace Newsletter.Core.Repositories
{
    public interface Users : IRepositoryBase<Domain.User>
    {
        Task<bool> DoesUserAlreadyExist(string email);
    }
}
