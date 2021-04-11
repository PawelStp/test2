using Games.Core.Entities.Games;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories.Games
{
    public interface IGameRepository
    {
        Task<Game> Get(long id, CancellationToken cancellationToken);
        Task Add(Game game, CancellationToken cancellationToken);
        Task<IList<Game>> QueryGames(QueryGamesParameters parameters, CancellationToken cancellationToken);
        Task<int> CountGames(QueryGamesParameters parameters, CancellationToken cancellationToken);
        Task Delete(Game game, CancellationToken cancellationToken);
        Task Update(Game game, CancellationToken cancellationToken);
    }
}
