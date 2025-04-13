using System.Text.Json;
using GameTracker.Models;

namespace GameTracker.Repositories;

public class JsonGameConsoleRepository : IGameConsoleRepository
{
    private readonly string _filePath;

    public JsonGameConsoleRepository()
    {
        _filePath = Path.Combine("./5-Data-Files/gameConsoles.json");
    }

    public List<GameConsole> GetAllGameConsoles()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<GameConsole>();

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<GameConsole>>(stream) ?? new List<GameConsole>();
        }

        catch
        {
            throw new Exception("Failed to retrive game consoles.");
        }
    }

    public List<GameConsole> GetGameConsoleByFilters(Dictionary<string, object> filters)
    {
        var gameConsoles = GetAllGameConsoles();

        foreach (var filter in filters)
        {
            switch (filter.Key.ToLower())
            {
                case "name":
                    gameConsoles = gameConsoles.Where(g => g.Name.Contains((string)filter.Value)).ToList();
                    break;
                case "condition":
                    if (filter.Value is string conditionStr && Enum.TryParse<GameConsoleCondition>(conditionStr, out var conditionEnum))
                    {
                        gameConsoles = gameConsoles.Where(g => g.Condition == conditionEnum).ToList();
                    }
                    break;
                case "developer":
                    gameConsoles = gameConsoles.Where(g => g.Developer.Contains((string)filter.Value)).ToList();
                    break;
                case "year":
                    if (filter.Value is int year)
                    {
                        gameConsoles = gameConsoles.Where(g => g.Year == year).ToList();
                    }
                    break;
                default:
                    break;
            }
        }
        return gameConsoles;
    }

    public void AddConsole(GameConsole gameConsole)
    {
        var gameConsoles = GetAllGameConsoles();
        gameConsoles.Add(gameConsole);
        SaveConsoles(gameConsoles);
    }

    public void UpdateConsole(GameConsole gameConsole)
    {
        var consoles = GetAllGameConsoles();
        var index = consoles.FindIndex(g => g.Name == gameConsole.Name && g.Condition == gameConsole.Condition);
        if (index >= 0)
        {
            consoles[index] = gameConsole;
            SaveConsoles(consoles);
        }
    }

    private void SaveConsoles(List<GameConsole> gameConsoles)
    {
        try
        {
            var json = JsonSerializer.Serialize(gameConsoles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            throw new Exception("Failed to save game consoles to file");
        }
    }
}