using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IWorkerService
{
    List<WorkerReadDTO> GetAllWorkers();
    WorkerReadDTO? GetWorkerById(string id);
    void AddWorker(WorkerCreateDTO workerCreateDTO);
    void UpdateWorker(Worker worker);
}