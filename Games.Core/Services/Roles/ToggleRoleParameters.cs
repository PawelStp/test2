using System;

namespace Games.Core.Services.Roles
{
    public class ToggleRoleParameters
    {
        public ToggleRoleParameters(long userId, string name)
        {
            UserId = userId <= 0 ? throw new ArgumentNullException(nameof(userId)) : userId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public long UserId { get; }
        public string Name { get; }
    }
}
