namespace GameTracker.Models;

public class Checkout
{
    public int Id { get; set; }
    public int WorkerId { get; set; } // FK to Worker
    public int GameId { get; set; }   // FK to Game
    public int GameConsoleId { get; set; } // FK to Console
    public DateTime CheckoutDate { get; set; } = DateTime.UtcNow;

    public Worker Worker { get; set; }    // Navigation property to Worker
    public Game Game { get; set; }        // Navigation property to Game
    public GameConsole GameConsole { get; set; }  // Navigation property to Console
}
public enum ItemType
{
    Game,
    GameConsole
}