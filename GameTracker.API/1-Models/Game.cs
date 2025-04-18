namespace GameTracker.Models;

public class Game
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string Name { get; set; }
    public GameCondition Condition { get; set; }
    public decimal Price { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }

    // Navigation property for Store (the related store)
    public Store Store { get; set; } // The related Store entity

    // Navigation property for the many-to-many relationship with Tag
    public ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();

    // Direct navigation to Tags through the GameTag relationship (optional)
    public ICollection<Tag> Tags => GameTags.Select(gt => gt.Tag).ToList();
}

public enum GameCondition
{
    New,
    Used,
    Damaged
}