using Games.Core.Entities.Users;
using Games.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class RoleMsSqlServerRepository : MsSqlServerBaseRepository<Role>, IRoleRepository
    {
        public RoleMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> GetByName(string name, CancellationToken cancellationToken)
        {
            return await Queryable().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}
