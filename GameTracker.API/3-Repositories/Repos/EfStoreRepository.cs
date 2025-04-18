using GameTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Repositories
{
    public class EfStoreRepository : IStoreRepository
    {
        private readonly GameTrackerDbContext _context;

        public EfStoreRepository(GameTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Store> GetStoreByIdAsync(int storeId)
        {
            return await _context.Stores
                .Include(s => s.Workers)
                .Include(s => s.Games)
                .Include(s => s.GameConsoles)
                .FirstOrDefaultAsync(s => s.Id == storeId);
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores
                .Include(s => s.Workers)
                .Include(s => s.Games)
                .Include(s => s.GameConsoles)
                .ToListAsync();
        }

        public async Task AddStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }
    }
}