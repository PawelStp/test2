using Games.Core.Entities.Games;
using Games.Core.Exceptions;
using Games.Core.Interfaces.Repositories;
using Games.Core.Interfaces.Repositories.Games;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Services
{
    public class GameManagementService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GameManagementService(IGameRepository gameRepository, ICategoryRepository categoryRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task Add(Game game, CancellationToken cancellationToken)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (!await _categoryRepository.Exists(game.CategoryId, cancellationToken)) throw new ValidationException(Error.CategoryNotExists);

            await _gameRepository.Add(game, cancellationToken);
        }

        public async Task Edit(Game game, CancellationToken cancellationToken)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (!await _categoryRepository.Exists(game.CategoryId, cancellationToken)) throw new ValidationException(Error.CategoryNotExists);

            var existingGame = await _gameRepository.Get(game.Id, cancellationToken);
            if (existingGame == null) throw new ValidationException(Error.NotFound);

            existingGame.CopyFrom(game);

            await _gameRepository.Update(existingGame, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.Get(id, cancellationToken);
            if (game == null) throw new ValidationException(Error.NotFound);


            await _gameRepository.Delete(game, cancellationToken);
        }
    }
}
