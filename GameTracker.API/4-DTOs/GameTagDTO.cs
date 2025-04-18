using GameTracker.Models;

namespace GameTracker.DTOs;

public class TagDTO
{
    public int TagId { get; set; }
    public string Name { get; set; }
}

public class TagCreateDTO
{
    public string Name { get; set; }
}

public class GameTagDTO
{
    public string GameName { get; set; }
    public List<string> TagNames { get; set; } = new();
}

public class GameTagCreateDTO
{
    public int GameId { get; set; }
    public List<string> TagNames { get; set; } = new();
}