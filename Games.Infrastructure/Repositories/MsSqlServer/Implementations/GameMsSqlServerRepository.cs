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

        protected override IQueryable<Game> Queryable()
        {
            return base.Queryable().Include(x => x.Category);
        }

        private IQueryable<Game> BuildQuery(QueryGamesParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return Queryable().Where(() => !string.IsNullOrWhiteSpace(parameters.Title), x => x.Title.Contains(parameters.Title))
                 .Where(() => !string.IsNullOrWhiteSpace(parameters.Description), x => x.Description.Contains(parameters.Description))
                 .Where(() => !string.IsNullOrWhiteSpace(parameters.Category), x => x.Category.Name.Contains(parameters.Category));
        }
    }
}
