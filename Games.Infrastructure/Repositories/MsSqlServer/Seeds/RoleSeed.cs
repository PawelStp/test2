using Games.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Games.Infrastructure.Repositories.MsSqlServer.Seeds
{
    internal static class RoleSeed
    {
        private static IDictionary<int, string> _roles = new Dictionary<int, string>() { { 1, "Admin" }, { 2, "User" }, { 3, "Editor" } };

        public static void Run(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(_roles.Select(x => new Role(x.Key, x.Value)));
        }
    }
}
