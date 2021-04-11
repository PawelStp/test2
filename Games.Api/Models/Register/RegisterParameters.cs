using System.ComponentModel.DataAnnotations;

namespace Games.Api.Models.Register
{
    public class RegisterParameters
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        internal Core.Services.Registration.RegisterParameters ToDomainParameters()
        {
            return new Core.Services.Registration.RegisterParameters(Username, FirstName, LastName, Email, Password);
        }
    }
}
