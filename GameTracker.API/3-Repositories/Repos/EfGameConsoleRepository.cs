using GameTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Repositories;

public class EfGameConsoleRepository : IGameConsoleRepository
{
    private readonly GameTrackerDbContext _context;

    public EfGameConsoleRepository(GameTrackerDbContext context)
    {
        _context = context;
    }

    // Get all game consoles
    public async Task<List<GameConsole>> GetAllGameConsolesAsync()
    {
        return await _context.GameConsoles
            .Include(g => g.Store)   // Assuming there's a Store navigation property
            .ToListAsync();
    }

    public async Task<GameConsole?> GetGameConsoleByIdAsync(int id)
    {
        return await _context.GameConsoles
            .Include(gc => gc.Store)
            .FirstOrDefaultAsync(gc => gc.Id == id);
    }

    // Get game consoles by filters
    public async Task<List<GameConsole>> GetGameConsolesByFiltersAsync(Dictionary<string, object> filters)
    {
        var query = _context.GameConsoles.AsQueryable();

        foreach (var filter in filters)
        {
            switch (filter.Key.ToLower())
            {
                case "name":
                    query = query.Where(g => g.Name.Contains((string)filter.Value));
                    break;
                case "condition":
                    if (filter.Value is string conditionStr && Enum.TryParse<GameConsoleCondition>(conditionStr, out var conditionEnum))
                    {
                        query = query.Where(g => g.Condition == conditionEnum);
                    }
                    break;
                case "developer":
                    query = query.Where(g => g.Developer.Contains((string)filter.Value));
                    break;
                case "year":
                    if (filter.Value is int year)
                    {
                        query = query.Where(g => g.Year == year);
                    }
                    break;
                default:
                    break;
            }
        }

        return await query.ToListAsync();
    }

    // Add a new game console
    public async Task AddGameConsoleAsync(GameConsole gameConsole)
    {
        await _context.GameConsoles.AddAsync(gameConsole);
        await _context.SaveChangesAsync();
    }

    // Method to remove a game console
    public async Task RemoveGameConsoleAsync(GameConsole gameConsole)
    {
        _context.GameConsoles.Remove(gameConsole);  // Remove the console from the context
        await _context.SaveChangesAsync();  // Save changes to the database
    }

    // Update an existing game console
    public async Task UpdateGameConsoleAsync(GameConsole gameConsole)
    {
        var existingConsole = await _context.GameConsoles
            .FirstOrDefaultAsync(g => g.Name == gameConsole.Name && g.Condition == gameConsole.Condition);

        if (existingConsole != null)
        {
            existingConsole.Name = gameConsole.Name;
            existingConsole.Condition = gameConsole.Condition;
            existingConsole.Developer = gameConsole.Developer;
            existingConsole.Year = gameConsole.Year;
            existingConsole.StoreId = gameConsole.StoreId;

            await _context.SaveChangesAsync();
        }
    }
}