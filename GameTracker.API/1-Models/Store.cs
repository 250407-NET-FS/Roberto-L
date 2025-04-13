namespace GameTracker.Models;

public class Store
{
    private string Id { get; set; } = Guid.NewGuid().ToString();
    private string Name { get; set; }
    private string Worker_Id { get; set; }
}