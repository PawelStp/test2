using Games.Core.Entities.Categories;
using Games.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class CategoryMsSqlServerRepository : MsSqlServerBaseRepository<Category>, ICategoryRepository
    {
        public CategoryMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<long>> GetHighlightByUserId(long userId, CancellationToken cancellationToken)
        {
            var a = await Queryable()
                .Where(x => x.Games.Any(y => y.Rates.Any(r => r.UserId == userId)))
                .ToListAsync();

            return a
             .GroupBy(x => x.Id)
             .Select(x => new
             {
                 id = x.Key,
                 rate = x.Sum(y => y.Games.Sum(x => x.Rates.Where(r => r.UserId == userId).FirstOrDefault()?.Value ?? 0))
             })
             .OrderByDescending(x => x.rate)
             .Take(3)
             .Select(x => x.id)
             .ToList();

        }

        protected override IQueryable<Category> Queryable(bool AsNotTracking = true)
        {
            return base.Queryable(AsNotTracking).Include(x => x.Games).ThenInclude(x => x.Rates);
        }
    }
}
