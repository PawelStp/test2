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

        internal void AddRole(Role role)
        {
            _roles.Add(new UserRole(this.Id, role.Id));
        }
    }
}
