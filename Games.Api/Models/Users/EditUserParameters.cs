using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Games.Api.Models.Users
{
    public class EditUserParameters
    {
        [Required]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        internal Core.Entities.Users.User ToDomainModel()
        {
            return new Core.Entities.Users.User(Id, FirstName, LastName, Password, Username, Email);
        }
    }
}
