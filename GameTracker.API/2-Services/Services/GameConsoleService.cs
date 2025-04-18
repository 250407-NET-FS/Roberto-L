using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Services;

public class GameConsoleService : IGameConsoleService
{
    private readonly IGameConsoleRepository _gameConsoleRepo;
    private readonly IStoreRepository _storeRepo;

    public GameConsoleService(IGameConsoleRepository gameConsoleRepo, IStoreRepository storeRepo)
    {
        _gameConsoleRepo = gameConsoleRepo;
        _storeRepo = storeRepo;
    }

    // Ensure method signatures match exactly with the interface
    public async Task<List<GameConsoleDTO>> GetAllGameConsolesAsync()
    {
        var consoles = await _gameConsoleRepo.GetAllGameConsolesAsync();
        return consoles.Select(gc => new GameConsoleDTO
        {
            Id = gc.Id,
            Name = gc.Name,
            Condition = gc.Condition
        }).ToList();
    }

    public async Task<GameConsoleDTO?> GetGameConsoleByIdAsync(int id)
    {
        var gc = await _gameConsoleRepo.GetGameConsoleByIdAsync(id);
        return gc == null ? null : new GameConsoleDTO
        {
            Id = gc.Id,
            Name = gc.Name,
            Condition = gc.Condition
        };
    }

    public async Task<List<GameConsoleDTO>> GetGameConsolesByFiltersAsync(Dictionary<string, object> filters)
    {
        var consoles = await _gameConsoleRepo.GetGameConsolesByFiltersAsync(filters);
        return consoles.Select(gc => new GameConsoleDTO
        {
            Id = gc.Id,
            Name = gc.Name,
            Condition = gc.Condition
        }).ToList();
    }


    // Adding missing AddGameConsoleAsync method
    public async Task AddGameConsoleAsync(GameConsoleDTO gameConsoleDTO)
    {
        // Validate input fields
        if (string.IsNullOrEmpty(gameConsoleDTO.Name))
            throw new ArgumentException("Game console name cannot be empty.");
        if (string.IsNullOrEmpty(gameConsoleDTO.Developer))
            throw new ArgumentException("Developer cannot be empty.");

        // Check if the store exists
        var store = await _storeRepo.GetStoreByIdAsync(gameConsoleDTO.StoreId);
        if (store == null)
            throw new Exception("Store not found.");

        // Create a new GameConsole object
        var newConsole = new GameConsole
        {
            StoreId = gameConsoleDTO.StoreId,
            Condition = gameConsoleDTO.Condition,
            Name = gameConsoleDTO.Name,
            Developer = gameConsoleDTO.Developer,  // Make sure Developer is being set
            Year = gameConsoleDTO.Year,            // If Year is available, set it
            Price = gameConsoleDTO.Price           // If Price is available, set it
        };

        // Call the repository to add the new game console
        await _gameConsoleRepo.AddGameConsoleAsync(newConsole);
    }
}