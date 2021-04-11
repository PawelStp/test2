using Games.Core.Entities.Games;
using Games.Core.Entities.Users;
using Games.Infrastructure.Repositories.MsSqlServer.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Games.Infrastructure.Repositories.MsSqlServer
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RoleSeed.Run(modelBuilder);
            CategorySeed.Run(modelBuilder);
        }
    }
}
