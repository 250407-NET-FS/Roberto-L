using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IGameConsoleService
{
    Task<List<GameConsoleDTO>> GetAllGameConsolesAsync();
    Task<GameConsoleDTO?> GetGameConsoleByIdAsync(int id);
    Task<List<GameConsoleDTO>> GetGameConsolesByFiltersAsync(Dictionary<string, object> filters);
    Task AddGameConsoleAsync(GameConsoleDTO gameConsoleDTO);
}