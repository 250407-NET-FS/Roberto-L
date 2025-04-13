namespace models.Game;

public class Game
{
    private string Id { get; set; } = Guid.NewGuid().ToString();
    private string Store_Id { get; set; }
    private int inventory { get; set; }
    private decimal Price { get; set; }
    private string Condition { get; set; }
    private string Publisher { get; set; }
    private string Developer { get; set; }
    private string Name { get; set; }
    private int Year { get; set; }
}