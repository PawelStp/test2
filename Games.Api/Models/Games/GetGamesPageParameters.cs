namespace Games.Api.Models.Games
{
    public class GetGamesPageParameters : GetPageBaseParameters
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        internal Core.Interfaces.Repositories.Games.QueryGamesParameters ToDomainParameters()
        {
            return new Core.Interfaces.Repositories.Games.QueryGamesParameters(Title, Description, Category, Size, PageIndex, OrderBy, IsDescending);
        }
    }
}
