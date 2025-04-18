using GameTracker.Models;

namespace GameTracker.DTOs;

public class GameConsoleDTO
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public GameConsoleCondition Condition { get; set; }
    public string Developer { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}