using System.ComponentModel.DataAnnotations.Schema;

namespace Games.Core.Entities.Users
{
    public class UserRole : BaseEntity
    {
        public UserRole(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        [ForeignKey(nameof(User))]
        public long UserId { get; private set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Role))]
        public long RoleId { get; private set; }
        public virtual Role Role { get; set; }
    }
}
