namespace GameTracker.Models;

public class Checkout
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ItemId { get; set; }
    public ItemType ItemType { get; set; }
    public string StoreId { get; set; }
    public int Quantity { get; set; }
    public DateTime CheckoutDate { get; set; } = DateTime.UtcNow;
}
public enum ItemType
{
    Game,
    GameConsole
}