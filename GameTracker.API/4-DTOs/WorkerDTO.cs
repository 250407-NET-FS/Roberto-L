using GameTracker.Models;

namespace GameTracker.DTOs;

public class WorkerCreateDTO
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Position { get; set; }
    public string StoreId { get; set; }
}

public class WorkerReadDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Position { get; set; }
}

public class WorkerUpdateDTO
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Position { get; set; }
}