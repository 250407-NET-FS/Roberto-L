using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IGameService
{
    Task<List<GameDTO>> GetAllGamesAsync();
    Task<List<GameDTO>> GetGamesByStoreIdAsync(int storeId);
    Task<GameDTO?> GetGameByIdAsync(int gameId);
    Task<Game> AddGameAsync(GameDTO gameDTO);
    Task RemoveGameAsync(int gameId);
    Task<List<GameDTO>> GetGamesByFiltersAsync(Dictionary<string, object> filters);
}