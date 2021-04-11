namespace Games.Core.Interfaces.Repositories.Games
{
    public class QueryGamesParameters : QueryBaseParameters
    {
        public QueryGamesParameters(string title, string description, string category, int? size, int? pageIndex, string orderBy, bool? isDescending) : base(size, pageIndex, orderBy, isDescending)
        {
            Title = title;
            Description = description;
            Category = category;
        }

        public string Title { get; }
        public string Description { get; }
        public string Category { get; }
    }
}
