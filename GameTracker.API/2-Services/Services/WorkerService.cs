using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using System.Threading.Tasks;

namespace GameTracker.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepo;

        public WorkerService(IWorkerRepository workerRepo)
        {
            _workerRepo = workerRepo;
        }

        // Get all workers asynchronously
        public async Task<List<WorkerReadDTO>> GetAllWorkersAsync()
        {
            var workers = await _workerRepo.GetAllWorkersAsync();
            return workers.Select(w => new WorkerReadDTO
            {
                Id = w.Id,
                Name = w.Name,
                Username = w.Username,
                Position = w.Position
            }).ToList();
        }

        // Get a worker by ID asynchronously
        public async Task<WorkerReadDTO?> GetWorkerByIdAsync(int id)
        {
            var worker = await _workerRepo.GetWorkerByIdAsync(id);
            if (worker != null)
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

        // Add a new worker asynchronously
        public async Task<WorkerReadDTO> AddWorkerAsync(WorkerCreateDTO workerDTO)
        {
            var worker = new Worker
            {
                Name = workerDTO.Name,
                Username = workerDTO.Username,
                Password = workerDTO.Password,
                Position = workerDTO.Position,
                StoreId = workerDTO.StoreId
            };

            Worker addedWorker = await _workerRepo.AddWorkerAsync(worker);

            return new WorkerReadDTO
            {
                Id = addedWorker.Id,
                Name = addedWorker.Name,
                Username = addedWorker.Username,
                Position = addedWorker.Position,
                StoreName = addedWorker.Store?.Name
            };
        }

        // Update a worker asynchronously
        public async Task UpdateWorkerAsync(int id, WorkerUpdateDTO workerUpdateDTO)
        {
            var worker = await _workerRepo.GetWorkerByIdAsync(id);
            if (worker != null)
            {
                worker.Name = workerUpdateDTO.Name;
                worker.Username = workerUpdateDTO.Username;
                worker.Position = workerUpdateDTO.Position;
                if (!string.IsNullOrEmpty(workerUpdateDTO.Password)) // Only update password if provided
                {
                    worker.Password = workerUpdateDTO.Password;
                }

                await _workerRepo.UpdateWorkerAsync(worker);
            }
            else
            {
                throw new Exception("Worker not found");
            }
        }
    }
}