using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

namespace GameTracker.Services;

public class GameConsoleService : IGameConsoleService
{
    private readonly IGameConsoleRepository _gameConsoleRepo;

    public GameConsoleService(IGameConsoleRepository gameConsoleRepo)
    {
        _gameConsoleRepo = gameConsoleRepo;
    }

    public void AddOrUpdateGameConsole(GameConsoleCreateDTO incomingGameConsoleDTO)
    {
        var existingGameConsole = _gameConsoleRepo.GetAllGameConsoles().FirstOrDefault(gc => gc.Name == incomingGameConsoleDTO.Name && gc.Condition == incomingGameConsoleDTO.Condition);
        if (existingGameConsole is not null)
        {
            existingGameConsole.Inventory += incomingGameConsoleDTO.Inventory;
            _gameConsoleRepo.UpdateGameConsole(existingGameConsole);
        }
        else
        {
            var newGameConsole = new GameConsole
            {
                Id = Guid.NewGuid().ToString(),
                StoreId = incomingGameConsoleDTO.Store_Id,
                Inventory = incomingGameConsoleDTO.Inventory,
                Price = incomingGameConsoleDTO.Price,
                Condition = incomingGameConsoleDTO.Condition,
                Developer = incomingGameConsoleDTO.Developer,
                Name = incomingGameConsoleDTO.Name,
                Year = incomingGameConsoleDTO.Year
            };

            _gameConsoleRepo.AddGameConsole(newGameConsole);
        }
    }

    public List<GameConsoleReadDTO> GetAllGameConsoles()
    {
        var consoles = _gameConsoleRepo.GetAllGameConsoles();
        return consoles.Select(gc => new GameConsoleReadDTO
        {
            Id = gc.Id,
            Name = gc.Name,
            Inventory = gc.Inventory,
            Condition = gc.Condition,
            Price = gc.Price
        }).ToList();
    }
    public List<GameConsoleReadDTO> GetGameConsolesByFilters(Dictionary<string, object> filters)
    {
        var gameConsoles = _gameConsoleRepo.GetGameConsoleByFilters(filters);
        return gameConsoles.Select(gc => new GameConsoleReadDTO
        {
            Id = gc.Id,
            Name = gc.Name,
            Inventory = gc.Inventory,
            Condition = gc.Condition,
            Price = gc.Price
        }).ToList();
    }
}