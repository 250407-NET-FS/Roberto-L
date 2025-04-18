using GameTracker.Models;

namespace GameTracker.DTOs;

public class CheckoutCreateDTO
{
    public int? GameId { get; set; }
    public int? GameConsoleId { get; set; }
    public int WorkerId { get; set; }
}

public class CheckoutReadDTO
{
    public int Id { get; set; }
    public int? GameId { get; set; }
    public int? GameConsoleId { get; set; }
    public int WorkerId { get; set; }
    public int Quantity { get; set; }
    public DateTime CheckoutDate { get; set; }
}