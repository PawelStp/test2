using System.Collections.Generic;
using System.Linq;

namespace Games.Api.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }

        internal static User Create(Core.Entities.Users.User user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Roles = user.Roles
                    .Select(r => r.Role.Name)
                    .ToList()
            };
        }
    }
}
