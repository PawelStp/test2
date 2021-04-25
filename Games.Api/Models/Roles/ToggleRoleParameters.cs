using System.ComponentModel.DataAnnotations;

namespace Games.Api.Models.Roles
{
    public class ToggleRoleParameters
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Name { get; set; }

        internal Core.Services.Roles.ToggleRoleParameters ToDomainParameters()
        {
            return new Core.Services.Roles.ToggleRoleParameters(UserId, Name);
        }
    }
}
