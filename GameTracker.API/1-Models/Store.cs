namespace GameTracker.Models;

public class Store
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string WorkerId { get; set; }
}