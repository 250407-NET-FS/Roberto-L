using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IWorkerRepository
{
    List<Worker> GetAllWorkers();
    Worker? GetWorkerById(string id);
    void AddWorker(Worker worker);
    void UpdateWorker(Worker worker);
}