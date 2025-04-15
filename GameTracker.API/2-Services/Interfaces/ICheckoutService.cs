using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface ICheckoutService
{
    // Add a new checkout and handle inventory update
    void AddCheckout(CheckoutCreateDTO checkoutDTO);

    // Get a checkout by ID
    CheckoutReadDTO? GetCheckoutById(string id);

    // Get all checkouts
    List<CheckoutReadDTO> GetAllCheckouts();
}