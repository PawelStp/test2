namespace Games.Api.Models.Users
{
    public class GetUsersPageParameters : GetPageBaseParameters
    {
        public string UserName { get; set; }

        internal Core.Interfaces.Repositories.Users.QueryUsersParameters ToDomainParameters()
        {
            return new Core.Interfaces.Repositories.Users.QueryUsersParameters(UserName, Size, PageIndex, OrderBy, IsDescending);
        }
    }
}
