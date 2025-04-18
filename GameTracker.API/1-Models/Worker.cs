namespace GameTracker.Models;
public class Worker
{
    public int Id { get; set; }
    public int StoreId { get; set; }  // StoreId added here for searching by StoreId
    public Store Store { get; set; }     // Navigation property to Store
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Position { get; set; } // Renamed 'Time' to 'Position' as it's more meaningful
    public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}