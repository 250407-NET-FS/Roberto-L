namespace GameTracker.Models;

public class Game
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StoreId { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public GameCondition Condition { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

public enum GameCondition
{
    New,
    Used,
    Damaged
}