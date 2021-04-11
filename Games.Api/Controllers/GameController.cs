using Games.Api.Models.Games;
using Games.Core.Interfaces.Repositories.Games;
using Games.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GameManagementService _gameManagementService;
        private readonly IGameRepository _gameRepository;

        public GameController(GameManagementService gameService, IGameRepository gameRepository)
        {
            _gameManagementService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
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
    }
}
