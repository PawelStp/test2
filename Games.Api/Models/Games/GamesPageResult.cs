using System.Collections.Generic;
using System.Linq;

namespace Games.Api.Models.Games
{
    public class GamesPageResult : PageBaseResult<Game>
    {
        public GamesPageResult(int count, int? pageIndex, IList<Core.Entities.Games.Game> games)
        {
            Data = games.Select(x => Game.Create(x)).ToList();
            Count = count;
            PageIndex = pageIndex ?? 1;
        }
    }
}
