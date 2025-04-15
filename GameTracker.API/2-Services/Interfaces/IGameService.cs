using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IGameService
{
    // Add or update a game based on the incoming DTO
    void AddOrUpdateGame(GameCreateDTO incomingGameDTO);

    // Get all games and return them as DTOs
    List<GameReadDTO> GetAllGames();

    // Get games by filters and return them as DTOs
    List<GameReadDTO> GetGamesByFilters(Dictionary<string, object> filters);
}