using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

namespace GameTracker.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _checkoutRepo;
    private readonly IGameRepository _gameRepo;
    private readonly IGameConsoleRepository _gameConsoleRepo;
    private readonly IWorkerRepository _workerRepo;

    public CheckoutService(ICheckoutRepository checkoutRepo, IGameRepository gameRepo, IGameConsoleRepository gameConsoleRepo, IWorkerRepository workerRepo)
    {
        _checkoutRepo = checkoutRepo;
        _gameRepo = gameRepo;
        _gameConsoleRepo = gameConsoleRepo;
        _workerRepo = workerRepo;
    }

    // This method is now named 'AddCheckoutAsync' to match the interface method
    public async Task<CheckoutReadDTO> AddCheckoutAsync(CheckoutCreateDTO dto)
    {
        // Validate that worker exists
        var worker = await _workerRepo.GetWorkerByIdAsync(dto.WorkerId);
        if (worker == null)
            throw new Exception("Worker not found.");

        // Validate that the game or console exists
        Game game = null;
        GameConsole gameConsole = null;

        if (dto.GameId.HasValue) // Check if GameId has a value
        {
            game = await _gameRepo.GetGameByIdAsync(dto.GameId.Value); // Use .Value to get the underlying value
            if (game == null)
                throw new Exception("Game not found.");
        }
        else if (dto.GameConsoleId.HasValue) // Check if GameConsoleId has a value
        {
            gameConsole = await _gameConsoleRepo.GetGameConsoleByIdAsync(dto.GameConsoleId.Value); // Use .Value to get the underlying value
            if (gameConsole == null)
                throw new Exception("Game console not found.");
        }

        // Proceed with checkout process
        var checkout = new Checkout
        {
            WorkerId = dto.WorkerId,
            GameId = dto.GameId ?? 0, // If GameId is null, assign 0 or a default value
            GameConsoleId = dto.GameConsoleId ?? 0, // If GameConsoleId is null, assign 0 or a default value
            CheckoutDate = DateTime.UtcNow // Assuming you want to set the current date
        };

        // Add checkout to the database (no inventory update here)
        await _checkoutRepo.AddCheckoutAsync(checkout);

        return new CheckoutReadDTO
        {
            Id = checkout.Id,
            WorkerId = checkout.WorkerId,
            GameId = checkout.GameId,
            GameConsoleId = checkout.GameConsoleId,
        };
    }

    // This method is now named 'GetCheckoutByIdAsync' to match the interface method
    public async Task<CheckoutReadDTO?> GetCheckoutByIdAsync(int id)
    {
        var checkout = await _checkoutRepo.GetCheckoutByIdAsync(id);
        if (checkout == null) return null;

        return new CheckoutReadDTO
        {
            Id = checkout.Id,
            GameId = checkout.GameId,
            GameConsoleId = checkout.GameConsoleId,
            WorkerId = checkout.WorkerId,
            CheckoutDate = checkout.CheckoutDate
        };
    }

    // This method is now named 'GetAllCheckoutsAsync' to match the interface method
    public async Task<List<CheckoutReadDTO>> GetAllCheckoutsAsync()
    {
        var checkouts = await _checkoutRepo.GetAllCheckoutsAsync();
        return checkouts.Select(c => new CheckoutReadDTO
        {
            Id = c.Id,
            GameId = c.GameId,
            GameConsoleId = c.GameConsoleId,
            WorkerId = c.WorkerId,
            CheckoutDate = c.CheckoutDate
        }).ToList();
    }
}