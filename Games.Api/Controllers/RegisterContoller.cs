using Games.Core.Services.Registration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("/api/register")]
    public class RegisterContoller : ControllerBase
    {
        private readonly RegistrationService _registrationService;

        public RegisterContoller(RegistrationService registrationService)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Models.Register.RegisterParameters parameters, CancellationToken cancellationToken)
        {
            await _registrationService.Register(parameters.ToDomainParameters(), cancellationToken);
            return Ok();
        }
    }
}
