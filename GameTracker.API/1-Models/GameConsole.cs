namespace GameTracker.Models;

public class GameConsole
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GameConsoleCondition Condition { get; set; }
    public string Developer { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public int StoreId { get; set; }

    public Store Store { get; set; }  // Navigation property to the Store model
}
public enum GameConsoleCondition
{
    New,
    Used,
    Damaged
}