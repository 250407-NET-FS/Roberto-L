namespace GameTracker.Models;
public class Worker()
{
    private string Id { get; set; } = Guid.NewGuid().ToString();
    private string StoreId { get; set; }
    private string Name { get; set; }
    private string User { get; set; }
    private string Password { get; set; }
    private string Time { get; set; }
    private string Position { get; set; }
}