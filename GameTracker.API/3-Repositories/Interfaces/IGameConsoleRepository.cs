using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameConsoleRepository
{
    // Get all game consoles
    Task<List<GameConsole>> GetAllGameConsolesAsync();

    // Get game consoles by filters
    Task<List<GameConsole>> GetGameConsolesByFiltersAsync(Dictionary<string, object> filters);

    // Add a new game console
    Task AddGameConsoleAsync(GameConsole gameConsole);

    // Update an existing game console
    Task UpdateGameConsoleAsync(GameConsole gameConsole);

    Task<GameConsole?> GetGameConsoleByIdAsync(int id);

    Task RemoveGameConsoleAsync(GameConsole gameConsole);
}