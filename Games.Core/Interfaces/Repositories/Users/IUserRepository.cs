using Games.Core.Entities.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameOrEmail(string username, string email, CancellationToken cancellationToken);
        Task Add(User user, CancellationToken cancellationToken);
        Task<User> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken);
        Task<User> Get(long id, CancellationToken cancellationToken);
        Task<IList<User>> QueryUsers(QueryUsersParameters parameters, CancellationToken cancellationToken);
        Task<int> CountUsers(QueryUsersParameters parameters, CancellationToken cancellationToken);
        Task Delete(User user, CancellationToken cancellationToken);
        Task Update(User user, CancellationToken cancellationToken);
    }
}
