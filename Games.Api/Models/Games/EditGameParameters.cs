using System.ComponentModel.DataAnnotations;

namespace Games.Api.Models.Games
{
    public class EditGameParameters : AddGameParameters
    {
        [Required]
        public long Id { get; set; }

        internal override Core.Entities.Games.Game ToDomainModel()
        {
            return new Core.Entities.Games.Game(Id, Title, Description, ReleaseDate, Url, CategoryId);
        }
    }
}
