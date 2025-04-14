using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

public class GameConsoleService : IGameConsoleService
{
    private readonly IGameConsoleRepository _gameConsoleRepo;

    public GameConsoleService(IGameConsoleRepository gameConsoleRepo)
    {
        _gameConsoleRepo = gameConsoleRepo;
    }

    public void AddOrUpdateGame(GameConsole incomingGameConsole)
    {
        var existingGameConsole = _gameConsoleRepo.GetAllGameConsoles().FirstOrDefault(g => g.Name == incomingGameConsole.Name && g.Condition == incomingGameConsole.Condition);
        if (existingGameConsole is not null)
        {
            existingGameConsole.Inventory += incomingGameConsole.Inventory;
            _gameConsoleRepo.UpdateGameConsole(existingGameConsole);
        }
        else
        {
            _gameConsoleRepo.AddGameConsole(incomingGameConsole);
        }
    }
}