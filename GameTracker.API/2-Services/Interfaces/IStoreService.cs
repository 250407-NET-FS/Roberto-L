using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface IStoreService
{
    // Add a new store
    Task<StoreDTO> AddStoreAsync(StoreDTO dto);

    // Get a store by its ID
    Task<StoreDTO> GetStoreByIdAsync(int storeId);

    // Get all stores
    Task<List<StoreDTO>> GetAllStoresAsync();

    // Update an existing store
    Task UpdateStoreAsync(StoreDTO storeDTO);
}