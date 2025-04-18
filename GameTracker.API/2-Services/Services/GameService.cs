using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Services;

public class GameService : IGameService
{

    private readonly IGameRepository _repo;
    private readonly GameTrackerDbContext _context;

    public GameService(GameTrackerDbContext context, IGameRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public async Task<List<GameDTO>> GetAllGamesAsync()
    {
        return await _context.Games
            .Select(g => new GameDTO
            {
                Id = g.Id,
                StoreId = g.StoreId,
                Name = g.Name,
                Price = g.Price,
                Condition = g.Condition.ToString(),
                Publisher = g.Publisher,
                Developer = g.Developer,
                Year = g.Year
            }).ToListAsync();
    }

    public async Task<List<GameDTO>> GetGamesByStoreIdAsync(int storeId)
    {
        return await _context.Games
            .Where(g => g.StoreId == storeId)
            .Select(g => new GameDTO
            {
                Id = g.Id,
                StoreId = g.StoreId,
                Name = g.Name,
                Price = g.Price,
                Condition = g.Condition.ToString(),
                Publisher = g.Publisher,
                Developer = g.Developer,
                Year = g.Year
            }).ToListAsync();
    }

    public async Task<GameDTO?> GetGameByIdAsync(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game == null) return null;

        return new GameDTO
        {
            Id = game.Id,
            StoreId = game.StoreId,
            Name = game.Name,
            Price = game.Price,
            Condition = game.Condition.ToString(),
            Publisher = game.Publisher,
            Developer = game.Developer,
            Year = game.Year
        };
    }

    public async Task<Game> AddGameAsync(GameDTO gameDTO)
    {
        // Check if the game name or other necessary properties are provided
        if (string.IsNullOrWhiteSpace(gameDTO.Name))
            throw new Exception("Game name is required.");

        var newGame = new Game
        {
            Name = gameDTO.Name,
            StoreId = gameDTO.StoreId,
            Price = gameDTO.Price,
            Condition = Enum.Parse<GameCondition>(gameDTO.Condition),
            Publisher = gameDTO.Publisher,
            Developer = gameDTO.Developer,
            Year = gameDTO.Year
        };

        // Add the new game to the database
        _context.Games.Add(newGame);
        await _context.SaveChangesAsync();

        // Return the newly added game (which now includes the generated Id)
        return newGame;
    }

    public async Task RemoveGameAsync(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<GameDTO>> GetGamesByFiltersAsync(Dictionary<string, object> filters)
    {
        IQueryable<Game> query = _context.Games.AsQueryable();

        foreach (var filter in filters)
        {
            switch (filter.Key.ToLower())
            {
                case "name":
                    query = query.Where(g => g.Name.Contains(filter.Value.ToString()!));
                    break;
                case "condition":
                    if (Enum.TryParse<GameCondition>(filter.Value.ToString(), out var condition))
                        query = query.Where(g => g.Condition == condition);
                    break;
                case "publisher":
                    query = query.Where(g => g.Publisher.Contains(filter.Value.ToString()!));
                    break;
                case "developer":
                    query = query.Where(g => g.Developer.Contains(filter.Value.ToString()!));
                    break;
                case "year":
                    if (int.TryParse(filter.Value.ToString(), out var year))
                        query = query.Where(g => g.Year == year);
                    break;
                case "storeid":
                    if (int.TryParse(filter.Value.ToString(), out var storeId))
                        query = query.Where(g => g.StoreId == storeId);
                    break;
            }
        }

        return await query
            .Select(g => new GameDTO
            {
                Id = g.Id,
                StoreId = g.StoreId,
                Name = g.Name,
                Price = g.Price,
                Condition = g.Condition.ToString(),
                Publisher = g.Publisher,
                Developer = g.Developer,
                Year = g.Year
            }).ToListAsync();
    }
}