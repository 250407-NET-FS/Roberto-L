using GameTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Repositories
{
    public interface IStoreRepository
    {
        Task<Store> GetStoreByIdAsync(int storeId);
        Task<List<Store>> GetAllStoresAsync();
        Task AddStoreAsync(Store store);
        Task UpdateStoreAsync(Store store);
    }
}