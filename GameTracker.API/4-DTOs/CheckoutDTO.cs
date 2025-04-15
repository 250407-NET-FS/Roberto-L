using GameTracker.Models;

namespace GameTracker.DTOs;

public class CheckoutCreateDTO
{
    public string ItemId { get; set; }
    public ItemType ItemType { get; set; }
    public string StoreId { get; set; }
    public int Quantity { get; set; }
}

public class CheckoutReadDTO
{
    public string Id { get; set; }
    public string ItemId { get; set; }
    public ItemType ItemType { get; set; }
    public string StoreId { get; set; }
    public int Quantity { get; set; }
    public DateTime CheckoutDate { get; set; }
}