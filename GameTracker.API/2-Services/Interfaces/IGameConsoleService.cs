using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IGameConsoleService
{
    // Add or update a game console, receiving a GameConsoleCreateDTO for creation
    void AddOrUpdateGameConsole(GameConsoleCreateDTO incomingGameConsoleDTO);

    // Get all game consoles, returning a list of GameConsoleReadDTOs
    List<GameConsoleReadDTO> GetAllGameConsoles();

    // Get game consoles by filters, returning a list of GameConsoleReadDTOs
    List<GameConsoleReadDTO> GetGameConsolesByFilters(Dictionary<string, object> filters);
}