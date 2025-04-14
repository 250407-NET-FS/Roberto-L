using GameTracker.Models;

namespace GameTracker.DTOs;

public class GameCreateDTO
{
    public string Store_Id { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public GameCondition Condition { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

public class GameReadDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Inventory { get; set; }
    public GameCondition Condition { get; set; }
    public decimal Price { get; set; }
}
