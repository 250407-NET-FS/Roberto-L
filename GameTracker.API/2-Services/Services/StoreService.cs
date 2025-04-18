using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepo;

        public StoreService(IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }

        // Map Store entity to StoreDTO
        private StoreDTO MapToDTO(Store store) => new StoreDTO
        {
            Id = store.Id,
            Name = store.Name,
            Workers = store.Workers
                   .Where(w => w.StoreId == store.Id)
                   .Select(w => new WorkerReadDTO { Id = w.Id, Name = w.Name })
                   .ToList(),
            Games = store.Games
                .Where(g => g.StoreId == store.Id)
                .Select(g => new GameDTO { Id = g.Id, Name = g.Name })
                .ToList(),
            GameConsoles = store.GameConsoles
                        .Where(gc => gc.StoreId == store.Id)
                        .Select(gc => new GameConsoleDTO { Id = gc.Id, Name = gc.Name })
                        .ToList()
        };

        // Add a new store
        public async Task<StoreDTO> AddStoreAsync(StoreDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Store name is required.");

            var store = new Store { Name = dto.Name };
            await _storeRepo.AddStoreAsync(store); // Delegate to the repository
            return new StoreDTO
            {
                Id = store.Id,
                Name = store.Name
            };
        }

        // Get a store by its ID
        public async Task<StoreDTO> GetStoreByIdAsync(int storeId)
        {
            var store = await _storeRepo.GetStoreByIdAsync(storeId);
            return store == null ? null : MapToDTO(store);
        }

        // Get all stores
        public async Task<List<StoreDTO>> GetAllStoresAsync()
        {
            var stores = await _storeRepo.GetAllStoresAsync();
            return stores.Select(MapToDTO).ToList();
        }

        // Update an existing store
        public async Task UpdateStoreAsync(StoreDTO storeDTO)
        {
            var store = await _storeRepo.GetStoreByIdAsync(storeDTO.Id);
            if (store != null)
            {
                store.Name = storeDTO.Name;
                await _storeRepo.UpdateStoreAsync(store);
            }
        }
    }
}