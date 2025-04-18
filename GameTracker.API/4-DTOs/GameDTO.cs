using GameTracker.Models;

namespace GameTracker.DTOs;

public class GameDTO
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Condition { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
    public int Year { get; set; }
}
