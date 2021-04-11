using Games.Api.Authentication;
using Games.Api.Models.Authentication;
using Games.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("/api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtGenerator _jwtGenerator;

        public AuthenticateController(IUserRepository userRepository, JwtGenerator jwtGenerator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        }


        [HttpPost]
        public async Task<ActionResult<AuthenticationResult>> Login([FromBody] AuthenticationParameters parameters, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAndPassword(parameters.Username, parameters.Password, cancellationToken);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _jwtGenerator.Generate(user);


            return Ok(new AuthenticationResult(token));
        }
    }
}
