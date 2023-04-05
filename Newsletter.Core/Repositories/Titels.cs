using Newsletter.Core.Repositories.Base;
using Newsletter.Domain;

namespace Newsletter.Core.Repositories
{
    public interface Titels : IRepositoryBase<Domain.Titel>
    {
        Task<bool> DoesTitelAlreadyExist(string shortName);
        Task<Titel> GetById(Guid id);
    }
}
