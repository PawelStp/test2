using Games.Core.Entities.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameOrEmail(string username, string email, CancellationToken cancellationToken);
        Task Add(User user, CancellationToken cancellationToken);
        Task<User> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken);
    }
}
