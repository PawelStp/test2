using Games.Core.Entities.Users;
using Games.Core.Interfaces.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override IQueryable<User> Queryable(bool AsNotTracking = true)
        {
            return base.Queryable().Include(x => x.Roles).ThenInclude(x => x.Role);
        }

        public async Task<IList<User>> QueryUsers(QueryUsersParameters parameters, CancellationToken cancellationToken)
        {
            return await BuildQuery(parameters)
                 .OrderBy(parameters.OrderBy, parameters.IsDescending)
                 .GetPage(parameters.PageIndex, parameters.Size)
                 .ToListAsync(cancellationToken);
        }

        private IQueryable<User> BuildQuery(QueryUsersParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return Queryable().Where(() => !string.IsNullOrWhiteSpace(parameters.Username), x => x.Username.Contains(parameters.Username));
        }

        public async Task<int> CountUsers(QueryUsersParameters parameters, CancellationToken cancellationToken)
        {
            return await BuildQuery(parameters).CountAsync();
        }
    }
}
