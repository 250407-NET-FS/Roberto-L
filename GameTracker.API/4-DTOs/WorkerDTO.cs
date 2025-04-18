namespace GameTracker.DTOs;

public class WorkerCreateDTO
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Position { get; set; }
    public int StoreId { get; set; }
}

public class WorkerReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Position { get; set; }
    public string? StoreName { get; set; }
    public List<int> StoreManagerStoreIds { get; set; } = new();
}

public class WorkerUpdateDTO
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Position { get; set; }
    public string Password { get; set; }
}