using System;

namespace Games.Core.Services.Games
{
    public class RateGameParameters
    {
        public RateGameParameters(string comment, double rate, long gameId, long userId)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            Rate = rate;
            GameId = gameId;
            UserId = userId;
        }

        public string Comment { get; set; }
        public double Rate { get; set; }
        public long GameId { get; set; }
        public long UserId { get; set; }
    }
}
