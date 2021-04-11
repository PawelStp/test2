using Games.Core.Entities.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByName(string name, CancellationToken cancellationToken);
    }
}
