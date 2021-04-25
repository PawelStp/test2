using Games.Core.Entities.Users;
using Games.Core.Interfaces.Repositories;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class UserRoleMsSqlServerRepository : MsSqlServerBaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext) { }
    }
}
