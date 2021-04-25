using Games.Core.Entities.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task Add(UserRole userRole, CancellationToken cancellationToken);
        Task Delete(UserRole userRole, CancellationToken cancellationToken);
    }
}
