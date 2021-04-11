using Games.Core.Entities.Categories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAll(CancellationToken cancellationToken);
        Task<Category> Get(long categoryId, CancellationToken cancellationToken);
        Task<bool> Exists(long categoryId, CancellationToken cancellationToken);
    }
}
