using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameRepository
{
    List<Game> GetAllGames();
    List<Game> GetGameByFilters(Dictionary<string, object> filters);
    void AddGame(Game game);
    void UpdateGame(Game game);
}