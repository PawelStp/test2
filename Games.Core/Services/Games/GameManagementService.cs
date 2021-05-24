using Games.Core.Entities.Games;
using Games.Core.Exceptions;
using Games.Core.Interfaces.Repositories;
using Games.Core.Interfaces.Repositories.Games;
using Games.Core.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Core.Services
{
    public class GameManagementService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRateRepository _rateRepository;

        public GameManagementService(IGameRepository gameRepository, ICategoryRepository categoryRepository, IRateRepository rateRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
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

        public async Task RateGame(RateGameParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var game = await _gameRepository.Get(parameters.GameId, cancellationToken);
            if(game == null) throw new ValidationException(Error.GameNotExists);

            game.Rate(new Rate(parameters.Comment, parameters.Rate, parameters.UserId, parameters.GameId));
            await _gameRepository.Update(game, cancellationToken);
        }

        public async Task<List<SuggestionDto>> GetSuggestions(long userId, CancellationToken cancellationToken)
        {
            if (userId == 0) throw new ArgumentException(nameof(userId));

            var suggestedIds = await _rateRepository.GetSuggestedIds(userId);
            if (suggestedIds == null || !suggestedIds.Any()) return new List<SuggestionDto>();

            var games = await _gameRepository.GetAll(cancellationToken);

            return games.Where(g => suggestedIds.Contains(g.Id))
                .Select(g => new SuggestionDto
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .ToList();
        }

        public class SuggestionDto
        {
            public long Id { get; set; }
            public string Title { get; set; }
        }
    }
}
