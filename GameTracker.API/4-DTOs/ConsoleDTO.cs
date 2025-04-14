using GameTracker.Models;

namespace GameTracker.DTOs;

public class GameConsoleCreateDTO
{
    public string Store_Id { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public GameConsoleCondition Condition { get; set; }

    public string Developer { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

public class GameConsoleReadDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Inventory { get; set; }
    public GameConsoleCondition Condition { get; set; }
    public decimal Price { get; set; }
}
