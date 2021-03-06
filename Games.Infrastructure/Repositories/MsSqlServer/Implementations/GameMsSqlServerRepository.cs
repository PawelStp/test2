using Games.Core.Entities.Games;
using Games.Core.Interfaces.Repositories.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class GameMsSqlServerRepository : MsSqlServerBaseRepository<Game>, IGameRepository
    {
        public GameMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext) { }

        public async Task<IList<Game>> QueryGames(QueryGamesParameters parameters, CancellationToken cancellationToken)
        {
            return await BuildQuery(parameters)
                 .OrderBy(parameters.OrderBy, parameters.IsDescending)
                 .GetPage(parameters.PageIndex, parameters.Size)
                 .ToListAsync();
        }

        public async Task<int> CountGames(QueryGamesParameters parameters, CancellationToken cancellationToken)
        {
            return await BuildQuery(parameters).CountAsync();
        }

        protected override IQueryable<Game> Queryable(bool AsNotTracking = true)
        {
            return base.Queryable(AsNotTracking)
                .Include(x => x.Category)
                .Include(x => x.Rates)
                .ThenInclude(x => x.User);
        }

        private IQueryable<Game> BuildQuery(QueryGamesParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return Queryable().Where(() => !string.IsNullOrWhiteSpace(parameters.Title), x => x.Title.Contains(parameters.Title))
                 .Where(() => !string.IsNullOrWhiteSpace(parameters.Description), x => x.Description.Contains(parameters.Description))
                 .Where(() => !string.IsNullOrWhiteSpace(parameters.Category), x => x.Category.Name.Contains(parameters.Category));
        }

        public async Task<IList<Game>> GetHighlitedAndNotRatedByUserIdAndCategoryId(long categoryId, long userId, int size, CancellationToken cancellationToken)
        {
            var games = await Queryable()
                .Where(x => !x.Rates.Any(y => y.UserId == userId) && x.CategoryId == categoryId)
                .ToListAsync();

            return games
                .GroupBy(x => x.Id)
                .Select(x => new
                {
                    game = x.FirstOrDefault(),
                    rates = x.SelectMany(y => y.Rates).Sum(x => x.Value)
                }).OrderByDescending(y => y.rates)
                .Select(x => x.game)
                .ToList();
        }
    }
}
