using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepo;

    public GameService(IGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public void AddOrUpdateGame(Game incomingGame)
    {
        var existingGame = _gameRepo.GetAllGames().FirstOrDefault(g => g.Name == incomingGame.Name && g.Condition == incomingGame.Condition);
        if (existingGame is not null)
        {
            existingGame.Inventory += incomingGame.Inventory;
            _gameRepo.UpdateGame(existingGame);
        }
        else
        {
            _gameRepo.AddGame(incomingGame);
        }
    }
}