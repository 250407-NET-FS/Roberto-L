namespace GameTracker.Models;

public class Store
{
    private string Id { get; set; } = Guid.NewGuid().ToString();
    private string Name { get; set; }
    private string WorkerId { get; set; }
}