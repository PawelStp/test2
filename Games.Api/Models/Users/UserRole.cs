namespace Games.Api.Models.Users
{
    public class UserRole
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        internal static UserRole Create(Core.Entities.Users.UserRole entity)
        {
            return new UserRole
            {
                Id = entity.Id,
                RoleId = entity.Id,
                UserId = entity.Id
            };
        }
    }
}
