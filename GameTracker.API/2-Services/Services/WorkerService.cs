using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

namespace GameTracker.Services;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _workerRepo;

    public WorkerService(IWorkerRepository workerRepo)
    {
        _workerRepo = workerRepo;
    }

    public List<WorkerReadDTO> GetAllWorkers()
    {
        var workers = _workerRepo.GetAllWorkers();
        return workers.Select(w => new WorkerReadDTO
        {
            Id = w.Id,
            Name = w.Name,
            Username = w.Username,
            Position = w.Position
        }).ToList();
    }

    public WorkerReadDTO? GetWorkerById(string id)
    {
        var worker = _workerRepo.GetWorkerById(id);
        if (worker is not null)
        {
            return new WorkerReadDTO
            {
                Id = worker.Id,
                Name = worker.Name,
                Username = worker.Username,
                Position = worker.Position
            };
        }
        return null;
    }

    public void AddWorker(WorkerCreateDTO workerCreateDTO)
    {
        var worker = new Worker
        {
            Id = Guid.NewGuid().ToString(),
            StoreId = workerCreateDTO.StoreId,
            Name = workerCreateDTO.Name,
            Username = workerCreateDTO.Username,
            Password = workerCreateDTO.Password,
            Position = workerCreateDTO.Position
        };

        _workerRepo.AddWorker(worker);
    }

    public void UpdateWorker(Worker worker)
    {
        _workerRepo.UpdateWorker(worker);
    }
}