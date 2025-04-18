using GameTracker.Models;

namespace GameTracker.Repositories;

public interface ICheckoutRepository
{
    // Get all checkouts
    Task<List<Checkout>> GetAllCheckoutsAsync();

    // Get a checkout by its ID
    Task<Checkout?> GetCheckoutByIdAsync(int id);

    // Add a new checkout
    Task AddCheckoutAsync(Checkout checkout);
}