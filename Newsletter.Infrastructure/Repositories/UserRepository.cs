using Microsoft.EntityFrameworkCore;
using Newsletter.Core.Repositories;
using Newsletter.Domain;
using Newsletter.Infrastructure.Persistence;
using Newsletter.Infrastructure.Repositories.Base;

namespace Newsletter.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, Users
    {
        public UserRepository(NewsletterDatastore dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetById(Guid id)
        {
            return await _dbContext
                .Users
                .Include(x => x.Subscriptions)
                    .ThenInclude(x => x.Titel)
                .SingleAsync(x => x.Id == id);
        }
        public async Task<bool> DoesUserAlreadyExist(string email)
        {
            return await _dbContext
                .Users
                .AnyAsync(x =>
                    x.Email.Equals(email)
                );
        }
    }
}
