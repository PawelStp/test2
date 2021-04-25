using Games.Core.Entities.Users;
using Games.Core.Exceptions;
using Games.Core.Interfaces.Repositories.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Services.Users
{
    public class UserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Edit(User user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = await _userRepository.Get(user.Id, cancellationToken);
            if (existingUser == null) throw new ValidationException(Error.NotFound);

            existingUser.CopyFrom(user);

            await _userRepository.Update(existingUser, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(id, cancellationToken);
            if (user == null) throw new ValidationException(Error.NotFound);

            await _userRepository.Delete(user, cancellationToken);
        }
    }
}
