namespace Games.Core.Interfaces.Repositories.Users
{
    public class QueryUsersParameters : QueryBaseParameters
    {
        public QueryUsersParameters(string username, int? size, int? pageIndex, string orderBy, bool? isDescending) : base(size, pageIndex, orderBy, isDescending)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
