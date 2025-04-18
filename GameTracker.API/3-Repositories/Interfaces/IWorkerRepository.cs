using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IWorkerRepository
{
    Task<List<Worker>> GetAllWorkersAsync();
    Task<Worker?> GetWorkerByIdAsync(int id);
    Task<Worker> AddWorkerAsync(Worker worker);
    Task UpdateWorkerAsync(Worker worker);
}