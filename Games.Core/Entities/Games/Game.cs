using Games.Core.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Games.Core.Entities.Games
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        private List<Rate> _rates;

        public Game(string title, string description, DateTimeOffset releaseDate, string url, long categoryId)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Url = url;
            CategoryId = categoryId;
            _rates = new List<Rate>();
        }

        public Game(long id, string title, string description, DateTimeOffset releaseDate, string url, long categoryId) : this(title, description, releaseDate, url, categoryId)
        {
            Id = id;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset ReleaseDate { get; private set; }
        public string Url { get; private set; }
        public double AverageRate => Rates.Any() ? Rates.Sum(x => x.Value) / Rates.Count() : 0;

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; private set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Rate> Rates
        {
            get => _rates.AsReadOnly();
            private set => _rates = value.ToList();
        }

        internal void CopyFrom(Game game)
        {
            Title = game.Title;
            Description = game.Description;
            ReleaseDate = game.ReleaseDate;
            Url = game.Url;
            CategoryId = game.CategoryId;
        }

        internal void Rate(Rate rate)
        {
            _rates.Add(rate);
        }
    }
}
