using GameTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Repositories
{
    public class EfWorkerRepository : IWorkerRepository
    {
        private readonly GameTrackerDbContext _context;

        public EfWorkerRepository(GameTrackerDbContext context)
        {
            _context = context;
        }

        // Get all workers asynchronously
        public async Task<List<Worker>> GetAllWorkersAsync()
        {
            return await _context.Workers.ToListAsync();
        }

        // Get a worker by ID asynchronously (int now)
        public async Task<Worker?> GetWorkerByIdAsync(int id)
        {
            return await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
        }

        // Add a new worker asynchronously
        public async Task<Worker> AddWorkerAsync(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        // Update a worker asynchronously
        public async Task UpdateWorkerAsync(Worker worker)
        {
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();
        }
    }
}