using Newsletter.Core.Repositories.Base;

namespace Newsletter.Core.Repositories
{
    public interface Newsletters : IRepositoryBase<Domain.Newsletter>
    {
        Task<int> GetNextVersionNumberForTitel(Guid titelId);
    }
}
