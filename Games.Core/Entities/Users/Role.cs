using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Games.Core.Entities.Users
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        private List<UserRole> _users;

        public Role(long id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Users = new List<UserRole>();
        }

        public string Name { get; private set; }

        public virtual ICollection<UserRole> Users
        {
            get => _users.AsReadOnly();
            private set => _users = value.ToList();
        }
    }
}
