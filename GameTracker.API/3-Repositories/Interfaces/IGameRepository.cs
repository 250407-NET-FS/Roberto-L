using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameRepository
{
    // Get all games
    Task<List<Game>> GetAllGamesAsync();

    // Get games by store id
    Task<List<Game>> GetGamesByStoreIdAsync(int storeId);

    // Get a single game by id
    Task<Game> GetGameByIdAsync(int gameId);

    // Get games by a list of ids
    Task<List<Game>> GetGamesByIdsAsync(List<int> gameIds);

    // Get games by filters
    Task<List<Game>> GetGamesByFiltersAsync(Dictionary<string, object> filters);

    // Add a new game
    Task AddGameAsync(Game game);

    // Update an existing game
    Task UpdateGameAsync(Game game);

    // Remove a game by id
    Task RemoveGameAsync(int gameId);


}