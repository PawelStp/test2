using Games.Core.Entities.Users;
using Games.Core.Exceptions;
using Games.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Services.Registration
{
    public class RegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public RegistrationService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task Register(RegisterParameters parameters, CancellationToken cancellationToken)
        {
            var exisitingUser = await _userRepository.GetByUsernameOrEmail(parameters.Username, parameters.Email, cancellationToken);
            if (exisitingUser != null) throw new ValidationException(Error.AlreadyExists);

            var user = new User(parameters.FirstName, parameters.LastName, parameters.Password, parameters.Username, parameters.Email);
            var role = await _roleRepository.GetByName("User", cancellationToken);
            user.AddRole(role);
            await _userRepository.Add(user, cancellationToken);
        }
    }
}
