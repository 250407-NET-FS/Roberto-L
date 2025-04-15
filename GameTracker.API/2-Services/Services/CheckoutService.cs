using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;

namespace GameTracker.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _checkoutRepo;
    private readonly IGameRepository _gameRepo;
    private readonly IGameConsoleRepository _gameConsoleRepo;

    public CheckoutService(ICheckoutRepository checkoutRepo, IGameRepository gameRepo, IGameConsoleRepository gameConsoleRepo)
    {
        _checkoutRepo = checkoutRepo;
        _gameRepo = gameRepo;
        _gameConsoleRepo = gameConsoleRepo;
    }

    // Add checkout (maps DTO to model)
    public void AddCheckout(CheckoutCreateDTO checkoutDTO)
    {
        // Get the item from the appropriate repository (game or console)
        if (checkoutDTO.ItemType == ItemType.Game)
        {
            var game = _gameRepo.GetAllGames().FirstOrDefault(g => g.Id == checkoutDTO.ItemId);
            if (game != null && game.Inventory >= checkoutDTO.Quantity)
            {
                game.Inventory -= checkoutDTO.Quantity;
                _gameRepo.UpdateGame(game);

                // Map DTO to model and add checkout
                var checkout = new Checkout
                {
                    ItemId = checkoutDTO.ItemId,
                    ItemType = checkoutDTO.ItemType,
                    StoreId = checkoutDTO.StoreId,
                    Quantity = checkoutDTO.Quantity,
                    CheckoutDate = System.DateTime.UtcNow
                };
                _checkoutRepo.AddCheckout(checkout);
            }
            else
            {
                throw new System.Exception("Not enough inventory to checkout this game");
            }
        }
        else if (checkoutDTO.ItemType == ItemType.GameConsole)
        {
            var gameConsole = _gameConsoleRepo.GetAllGameConsoles().FirstOrDefault(gc => gc.Id == checkoutDTO.ItemId);
            if (gameConsole != null && gameConsole.Inventory >= checkoutDTO.Quantity)
            {
                gameConsole.Inventory -= checkoutDTO.Quantity;
                _gameConsoleRepo.UpdateGameConsole(gameConsole);

                // Map DTO to model and add checkout
                var checkout = new Checkout
                {
                    ItemId = checkoutDTO.ItemId,
                    ItemType = checkoutDTO.ItemType,
                    StoreId = checkoutDTO.StoreId,
                    Quantity = checkoutDTO.Quantity,
                    CheckoutDate = System.DateTime.UtcNow
                };
                _checkoutRepo.AddCheckout(checkout);
            }
            else
            {
                throw new System.Exception("Not enough inventory to checkout this game console");
            }
        }
    }

    // Get all checkouts, map model to DTO
    public List<CheckoutReadDTO> GetAllCheckouts()
    {
        var checkouts = _checkoutRepo.GetAllCheckouts();
        return checkouts.Select(c => new CheckoutReadDTO
        {
            Id = c.Id,
            ItemId = c.ItemId,
            ItemType = c.ItemType,
            StoreId = c.StoreId,
            Quantity = c.Quantity,
            CheckoutDate = c.CheckoutDate
        }).ToList();
    }

    // Get checkout by ID, map model to DTO
    public CheckoutReadDTO? GetCheckoutById(string id)
    {
        var checkout = _checkoutRepo.GetCheckoutById(id);
        if (checkout == null) return null;

        return new CheckoutReadDTO
        {
            Id = checkout.Id,
            ItemId = checkout.ItemId,
            ItemType = checkout.ItemType,
            StoreId = checkout.StoreId,
            Quantity = checkout.Quantity,
            CheckoutDate = checkout.CheckoutDate
        };
    }
}