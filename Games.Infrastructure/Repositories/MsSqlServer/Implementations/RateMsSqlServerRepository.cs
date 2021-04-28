using Games.Core.Entities.Games;
using Games.Core.Interfaces.Repositories;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class RateMsSqlServerRepository : MsSqlServerBaseRepository<Rate>, IRateRepository
    {
        public RateMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext) { }
    }
}
