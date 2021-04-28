using Games.Api.Authentication;
using Games.Api.Models.Games;
using Games.Core.Interfaces.Repositories.Games;
using Games.Core.Interfaces.Repositories.Users;
using Games.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("api/games"), Authorize]
    public class GameController : ControllerBase
    {
        private readonly GameManagementService _gameManagementService;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public GameController(GameManagementService gameService, IGameRepository gameRepository, IUserRepository userRepository)
        {
            _gameManagementService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<ActionResult<Game>> Get([FromQuery] long id, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.Get(id, cancellationToken);
            return Ok(Game.Create(game));
        }

        [HttpGet("search")]
        public async Task<ActionResult<GamesPageResult>> GetGamesPage([FromQuery] GetGamesPageParameters parameters, CancellationToken cancellationToken)
        {
            var domainParameters = parameters.ToDomainParameters();

            var games = await _gameRepository.QueryGames(domainParameters, cancellationToken);
            var count = await _gameRepository.CountGames(domainParameters, cancellationToken);

            return Ok(new GamesPageResult(count, parameters.PageIndex, games));
        }

        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] AddGameParameters parameters, CancellationToken cancellationToken)
        {
            await _gameManagementService.Add(parameters.ToDomainModel(), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGame([FromQuery] int id, CancellationToken cancellationToken)
        {
            await _gameManagementService.Delete(id, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGame([FromBody] EditGameParameters parameters, CancellationToken cancellationToken)
        {
            await _gameManagementService.Edit(parameters.ToDomainModel(), cancellationToken);
            return Ok();
        }

        [HttpPost("{gameId}/rate")]
        public async Task<ActionResult> RateGame(long gameId, [FromBody] RateGameParameters parameters, CancellationToken cancellationToken)
        {
            await _gameManagementService.RateGame(parameters.ToDomainModel(gameId, User.GetUserId()), cancellationToken);
            return Ok();
        }
    }
}
