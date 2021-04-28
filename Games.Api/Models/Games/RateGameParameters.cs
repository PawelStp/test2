using System;
using System.ComponentModel.DataAnnotations;

namespace Games.Api.Models.Games
{
    public class RateGameParameters
    {
        [Required]
        public string Comment { get; set; }
        public double Rate { get; set; }

        internal Core.Services.Games.RateGameParameters ToDomainModel(long gameId, long userId)
        {
            return new Core.Services.Games.RateGameParameters(Comment, Rate, gameId, userId);
        }
    }
}
