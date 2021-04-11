using Games.Core.Entities.Users;
using Games.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class UserMsSqlServerRepository : MsSqlServerBaseRepository<User>, IUserRepository
    {
        public UserMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext) { }

        public async Task<User> GetByUsernameOrEmail(string username, string email, CancellationToken cancellationToken)
        {
            return await Queryable().FirstOrDefaultAsync(x => x.Username == username || email == x.Email);
        }

        public async Task<User> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken)
        {
            return await Queryable().FirstOrDefaultAsync(x => x.Username == username && x.Password == password, cancellationToken);
        }
    }
}
