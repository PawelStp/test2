using System;
using System.Collections.Generic;
using System.Linq;

namespace Games.Api.Models.Games
{
    public class Game
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Url { get; set; }
        public double AverageRate { get; set; }
        public Category.Category Category { get; set; }

        public IList<RateComment> Rates { get; set; }

        internal static Game Create(Core.Entities.Games.Game game)
        {
            return game == null ? default : new Game
            {
                Id = game.Id,
                AverageRate = game.AverageRate,
                Category = new Category.Category() { Id = game.Category.Id, Name = game.Category.Name },
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
                Title = game.Title,
                Url = game.Url,
                Rates = game.Rates.Select(x => new RateComment
                {
                    Comment = x.Comment,
                    Rate = x.Value,
                    User = x.User.Username,
                    Date = x.Date
                }).ToList()
            };
        }
    }

    public class RateComment
    {
        public string Comment { get; set; }
        public double Rate { get; set; }
        public DateTimeOffset Date { get; set; }
        public string User { get; set; }
    }
}
