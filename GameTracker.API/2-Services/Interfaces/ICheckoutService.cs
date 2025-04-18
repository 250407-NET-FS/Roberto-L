using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface ICheckoutService
{
    Task<CheckoutReadDTO> AddCheckoutAsync(CheckoutCreateDTO dto);
    Task<CheckoutReadDTO?> GetCheckoutByIdAsync(int id);
    Task<List<CheckoutReadDTO>> GetAllCheckoutsAsync();
}