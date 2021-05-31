using Games.Api.Models.Users;
using Games.Core.Interfaces.Repositories.Users;
using Games.Core.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserManagementService _userManagementService;
        private readonly IUserRepository _userRepository;

        public UserController(UserManagementService userManagementService
            ,IUserRepository userRepository)
        {
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<ActionResult<User>> Get([FromQuery] long id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(id, cancellationToken);
            return Ok(Api.Models.Users.User.Create(user));
        }

        [HttpGet("all")]
        [Authorize("Admin")]
        public async Task<ActionResult<UsersPageResult>> GetUsersPage([FromQuery] GetUsersPageParameters parameters, CancellationToken cancellationToken)
        {
            var domainParameters = parameters.ToDomainParameters();

            var users = await _userRepository.QueryUsers(domainParameters, cancellationToken);
            var count = await _userRepository.CountUsers(domainParameters, cancellationToken);

            return Ok(new UsersPageResult(count, parameters.PageIndex, users));
        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<ActionResult> Delete([FromQuery] int id, CancellationToken cancellationToken)
        {
            await _userManagementService.Delete(id, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        [Authorize("Admin")]
        public async Task<ActionResult> Put([FromBody] EditUserParameters parameters, CancellationToken cancellationToken)
        {
            await _userManagementService.Edit(parameters.ToDomainModel(), cancellationToken);
            return Ok();
        }
    }
}
