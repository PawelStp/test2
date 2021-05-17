using Games.Core.Entities.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Games.Core.Entities.Users
{
    [Table("Users")]
    public class User : BaseEntity
    {
        private List<UserRole> _roles;
        public User(string firstName, string lastName, string password, string username, string email)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Roles = new List<UserRole>();
        }

        public User(long id, string firstName, string lastName, string password, string username, string email) : this(firstName, lastName, password, username, email)
        {
            Id = id;
        }

        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set; }
        [Required]
        public string Password { get; private set; }
        [Required]
        public string Username { get; private set; }
        [Required]
        public string Email { get; private set; }

        public virtual ICollection<UserRole> Roles
        {
            get => _roles.AsReadOnly();
            private set => _roles = value.ToList();
        }

        public virtual ICollection<Rate> Rates { get; set; }

        internal void AddRole(Role role)
        {
            _roles.Add(new UserRole(this.Id, role.Id));
        }

        internal UserRole GetRole(Role role)
            => _roles.FirstOrDefault(r => r.RoleId == role.Id);

        internal void CopyFrom(User user)
        {
            Email = user.Email;
            FirstName = user.Email;
            LastName = user.LastName;
            Password = user.Password;
            Username = user.Username;
        }
    }
}
