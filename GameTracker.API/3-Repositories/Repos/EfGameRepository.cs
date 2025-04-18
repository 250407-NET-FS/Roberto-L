using GameTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Repositories
{
    public class EfGameRepository : IGameRepository
    {
        private readonly GameTrackerDbContext _context;

        public EfGameRepository(GameTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games
                .Include(g => g.Store)
                .Include(g => g.Tags)
                .ToListAsync();
        }

        public async Task<List<Game>> GetGamesByStoreIdAsync(int storeId)
        {
            return await _context.Games
                .Where(g => g.StoreId == storeId)
                .Include(g => g.Store)
                .Include(g => g.Tags)
                .ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            return await _context.Games
                .Include(g => g.Store)
                .Include(g => g.Tags)
                .FirstOrDefaultAsync(g => g.Id == gameId)
                ?? throw new KeyNotFoundException($"Game with ID {gameId} not found.");
        }

        public async Task<List<Game>> GetGamesByIdsAsync(List<int> gameIds)
        {
            return await _context.Games
                .Where(g => gameIds.Contains(g.Id))
                .Include(g => g.Store)
                .Include(g => g.Tags)
                .ToListAsync();
        }

        public async Task<List<Game>> GetGamesByFiltersAsync(Dictionary<string, object> filters)
        {
            var query = _context.Games.AsQueryable();

            foreach (var filter in filters)
            {
                switch (filter.Key.ToLower())
                {
                    case "name":
                        query = query.Where(g => g.Name.Contains((string)filter.Value));
                        break;
                    case "condition":
                        if (filter.Value is string conditionStr && Enum.TryParse<GameCondition>(conditionStr, out var conditionEnum))
                        {
                            query = query.Where(g => g.Condition == conditionEnum);
                        }
                        break;
                    case "developer":
                        query = query.Where(g => g.Developer.Contains((string)filter.Value));
                        break;
                    case "publisher":
                        query = query.Where(g => g.Publisher.Contains((string)filter.Value));
                        break;
                    case "year":
                        if (filter.Value is int year)
                        {
                            query = query.Where(g => g.Year == year);
                        }
                        break;
                    case "storeid":
                        if (filter.Value is int storeId)
                        {
                            query = query.Where(g => g.StoreId == storeId);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await query
                .Include(g => g.Store)
                .Include(g => g.Tags)
                .ToListAsync();
        }

        public async Task AddGameAsync(Game game)
        {
            if (game.Id == 0 || !await _context.Games.AnyAsync(g => g.Id == game.Id))
            {
                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateGameAsync(Game game)
        {
            var existingGame = await _context.Games.FindAsync(game.Id);
            if (existingGame != null)
            {
                // EF Core automatically tracks changes, no need to manually set each property
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveGameAsync(int gameId)
        {
            var gameToRemove = await _context.Games.FindAsync(gameId);
            if (gameToRemove != null)
            {
                _context.Games.Remove(gameToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}