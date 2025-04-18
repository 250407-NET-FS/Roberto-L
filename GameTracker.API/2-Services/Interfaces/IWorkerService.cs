using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IWorkerService
{
    // Get all workers asynchronously
    Task<List<WorkerReadDTO>> GetAllWorkersAsync();

    // Get a worker by ID asynchronously
    Task<WorkerReadDTO?> GetWorkerByIdAsync(int id);

    // Add a new worker asynchronously
    Task<WorkerReadDTO> AddWorkerAsync(WorkerCreateDTO workerDTO);

    // Update a worker asynchronously
    Task UpdateWorkerAsync(int id, WorkerUpdateDTO workerUpdateDTO);
}