using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

namespace GameTracker.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepo;

    public GameService(IGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public void AddOrUpdateGame(GameCreateDTO incomingGameDTO)
    {
        // Convert the incoming DTO to the model
        var existingGame = _gameRepo.GetAllGames().FirstOrDefault(g => g.Name == incomingGameDTO.Name && g.Condition == incomingGameDTO.Condition);
        if (existingGame is not null)
        {
            existingGame.Inventory += incomingGameDTO.Inventory;
            _gameRepo.UpdateGame(existingGame);
        }
        else
        {
            var newGame = new Game
            {
                Id = Guid.NewGuid().ToString(),
                StoreId = incomingGameDTO.Store_Id,
                Inventory = incomingGameDTO.Inventory,
                Price = incomingGameDTO.Price,
                Condition = incomingGameDTO.Condition,
                Publisher = incomingGameDTO.Publisher,
                Developer = incomingGameDTO.Developer,
                Name = incomingGameDTO.Name,
                Year = incomingGameDTO.Year
            };

            _gameRepo.AddGame(newGame);
        }
    }

    public List<GameReadDTO> GetAllGames()
    {
        var games = _gameRepo.GetAllGames();
        return games.Select(g => new GameReadDTO
        {
            Id = g.Id,
            Name = g.Name,
            Inventory = g.Inventory,
            Condition = g.Condition,
            Price = g.Price
        }).ToList();
    }

    public List<GameReadDTO> GetGamesByFilters(Dictionary<string, object> filters)
    {
        var games = _gameRepo.GetGameByFilters(filters);
        return games.Select(g => new GameReadDTO
        {
            Id = g.Id,
            Name = g.Name,
            Inventory = g.Inventory,
            Condition = g.Condition,
            Price = g.Price
        }).ToList();
    }
}