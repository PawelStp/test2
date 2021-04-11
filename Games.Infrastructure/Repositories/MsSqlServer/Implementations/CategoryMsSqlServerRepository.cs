using Games.Core.Entities.Categories;
using Games.Core.Interfaces.Repositories;

namespace Games.Infrastructure.Repositories.MsSqlServer.Implementations
{
    internal class CategoryMsSqlServerRepository : MsSqlServerBaseRepository<Category>, ICategoryRepository
    {
        public CategoryMsSqlServerRepository(GamesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
