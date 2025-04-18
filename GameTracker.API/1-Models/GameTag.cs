namespace GameTracker.Models;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<GameTag> GameTags { get; set; } = new();
}
public class GameTag
{
    public int GameId { get; set; }
    public Game Game { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}