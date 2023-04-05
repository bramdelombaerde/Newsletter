using Newsletter.Core.Repositories.Base;

namespace Newsletter.Core.Repositories
{
    public interface Titels : IRepositoryBase<Domain.Titel>
    {
        Task<bool> DoesTitelAlreadyExist(string shortName);
    }
}
