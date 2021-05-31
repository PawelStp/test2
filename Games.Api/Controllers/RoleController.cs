using Games.Core.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("/api/role")]
    public class RoleController : ControllerBase
    {
        private readonly RolesManagementService _rolesManagementService;

        public RoleController(RolesManagementService rolesManagementService)
        {
            _rolesManagementService = rolesManagementService ?? throw new ArgumentNullException(nameof(rolesManagementService)); ;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Toggle([FromBody] Models.Roles.ToggleRoleParameters parameters, CancellationToken cancellationToken)
        {
            await _rolesManagementService.ToggleRole(parameters.ToDomainParameters(), cancellationToken);
            return Ok();
        }
    }
}
