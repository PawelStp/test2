using Games.Core.Entities.Categories;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Games.Core.Entities.Games
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        public Game(string title, string description, DateTimeOffset releaseDate, string url, long categoryId)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Url = url;
            CategoryId = categoryId;
        }

        public Game(long id, string title, string description, DateTimeOffset releaseDate, string url, long categoryId) : this(title, description, releaseDate, url, categoryId)
        {
            Id = id;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset ReleaseDate { get; private set; }
        public string Url { get; private set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; private set; }
        public virtual Category Category { get; set; }

        internal void CopyFrom(Game game)
        {
            Title = game.Title;
            Description = game.Description;
            ReleaseDate = game.ReleaseDate;
            Url = game.Url;
            CategoryId = game.CategoryId;
        }
    }
}
