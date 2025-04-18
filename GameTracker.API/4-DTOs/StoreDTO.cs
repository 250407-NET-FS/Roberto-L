using GameTracker.Models;
using GameTracker.DTOs;

namespace GameTracker.DTOs;

public class StoreDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<WorkerReadDTO> Workers { get; set; } = new();
    public List<GameDTO> Games { get; set; } = new();
    public List<GameConsoleDTO> GameConsoles { get; set; } = new();
}