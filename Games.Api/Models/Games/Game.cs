using System;

namespace Games.Api.Models.Games
{
    public class Game
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Url { get; set; }
        public decimal AverageRate { get; set; }
        public Category.Category Category { get; set; }

        internal static Game Create(Core.Entities.Games.Game game)
        {
            return new Game
            {
                Id = game.Id,
                AverageRate = 0,
                Category = new Category.Category() { Id = game.Category.Id, Name = game.Category.Name },
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
                Title = game.Title,
                Url = game.Url
            };
        }
    }
}
