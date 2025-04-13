using System.Text.Json;
using GameTracker.Models;

namespace GameTracker.Repositories;

public class JsonGameRepository : IGameRepository
{
    private readonly string _filePath;

    public JsonGameRepository()
    {
        _filePath = Path.Combine("./5-Data-Files/games.json");
    }

    public List<Game> GetAllGames()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Game>();

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Game>>(stream) ?? new List<Game>();
        }

        catch
        {
            throw new Exception("Failed to retrive games.");
        }
    }

    public List<Game> GetGameByFilters(Dictionary<string, object> filters)
    {
        var games = GetAllGames();

        foreach (var filter in filters)
        {
            switch (filter.Key.ToLower())
            {
                case "name":
                    games = games.Where(g => g.Name.Contains((string)filter.Value)).ToList();
                    break;
                case "condition":
                    if (filter.Value is string conditionStr && Enum.TryParse<GameCondition>(conditionStr, out var conditionEnum))
                    {
                        games = games.Where(g => g.Condition == conditionEnum).ToList();
                    }
                    break;
                case "publisher":
                    games = games.Where(g => g.Publisher.Contains((string)filter.Value)).ToList();
                    break;
                case "developer":
                    games = games.Where(g => g.Developer.Contains((string)filter.Value)).ToList();
                    break;
                case "year":
                    if (filter.Value is int year)
                    {
                        games = games.Where(g => g.Year == year).ToList();
                    }
                    break;
                default:
                    break;
            }
        }
        return games;
    }

    public void AddGame(Game game)
    {
        var games = GetAllGames();
        games.Add(game);
        SaveGames(games);
    }

    public void UpdateGame(Game game)
    {
        var games = GetAllGames();
        var index = games.FindIndex(g => g.Name == game.Name && g.Condition == game.Condition);
        if (index >= 0)
        {
            games[index] = game;
            SaveGames(games);
        }
    }

    private void SaveGames(List<Game> games)
    {
        try
        {
            var json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            throw new Exception("Failed to save games to file");
        }
    }
}