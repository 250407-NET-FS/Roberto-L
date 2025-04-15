namespace GameTracker.Models;
public class Worker()
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StoreId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Time { get; set; }
    public string Position { get; set; }
}