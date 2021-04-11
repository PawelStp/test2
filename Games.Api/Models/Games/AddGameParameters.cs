using System;
using System.ComponentModel.DataAnnotations;

namespace Games.Api.Models.Games
{
    public class AddGameParameters
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset ReleaseDate { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public long CategoryId { get; set; }

        internal virtual Core.Entities.Games.Game ToDomainModel()
        {
            return new Core.Entities.Games.Game(Title, Description, ReleaseDate, Url, CategoryId);
        }
    }
}
