using Games.Core.Entities.Games;
using Games.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class RateMsSqlServerRepository : MsSqlServerBaseRepository<Rate>, IRateRepository
    {
        public RateMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<Rate> Queryable(bool AsNotTracking = true)
        {
            return base.Queryable()
                .Include(r => r.User)
                .Include(r => r.Game)
                .ThenInclude(g => g.Category);
        }

        public async Task<List<long>> GetSuggestedIds(long userId)
        {
            var userRatesGameIds = await GetUserRatesGameIds(userId);

            return await Queryable()
                .Where(r => r.UserId != userId 
                    && !userRatesGameIds.Contains(r.GameId))
                .GroupBy(r => r.GameId)
                .Select(r => new
                {
                    GameId = r.Key,
                    Count = r.Count()
                })
                .OrderByDescending(r => r.Count)
                .Take(5)
                .Select(r => r.GameId)
                .ToListAsync();
        }

        private async Task<List<long>> GetUserRatesGameIds(long userId)
        {
            return await Queryable()
                .Where(r => r.UserId == userId)
                .Select(r => r.GameId)
                .Distinct()
                .ToListAsync();
        }
    }
}
