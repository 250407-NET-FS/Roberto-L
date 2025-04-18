namespace GameTracker.Models;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    public ICollection<Game> Games { get; set; } = new List<Game>();
    public ICollection<GameConsole> GameConsoles { get; set; } = new List<GameConsole>();
}