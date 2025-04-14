using GameTracker.Models;

namespace GameTracker.Repositories;

public interface ICheckoutRepository
{
    List<Checkout> GetAllCheckouts();
    Checkout? GetCheckoutById(string id);
    void AddCheckout(Checkout checkout);
}