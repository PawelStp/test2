using Games.Core.Entities.Users;
using Games.Core.Exceptions;
using Games.Core.Interfaces.Repositories;
using Games.Core.Interfaces.Repositories.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Services.Roles
{
    public class RolesManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public RolesManagementService(IUserRepository userRepository
            ,IRoleRepository roleRepository
            ,IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task ToggleRole(ToggleRoleParameters parameters, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.Get(parameters.UserId, cancellationToken);
            if (existingUser == null) throw new ValidationException(Error.NotFound);

            var role = await _roleRepository.GetByName(parameters.Name, cancellationToken);
            if (role == null) throw new ValidationException(Error.NotFound);

            var userRole = existingUser.GetRole(role);

            if (userRole == null)
                await _userRoleRepository.Add(new UserRole(parameters.UserId, role.Id), cancellationToken);
            else
                await _userRoleRepository.Delete(userRole, cancellationToken);
        }
    }
}
