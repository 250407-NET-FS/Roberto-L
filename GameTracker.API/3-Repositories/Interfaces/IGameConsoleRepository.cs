using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameConsoleRepository
{
    List<GameConsole> GetAllGameConsoles();
    public List<GameConsole> GetGameConsoleByFilters(Dictionary<string, object> filters);
    void AddConsole(GameConsole gameConsole);
    void UpdateConsole(GameConsole gameConsole);
}