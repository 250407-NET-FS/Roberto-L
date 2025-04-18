using GameTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameTracker.Repositories;

public class EfCheckoutRepository : ICheckoutRepository
{
    private readonly GameTrackerDbContext _context;

    public EfCheckoutRepository(GameTrackerDbContext context)
    {
        _context = context;
    }

    // Get all checkouts
    public async Task<List<Checkout>> GetAllCheckoutsAsync()
    {
        return await _context.Checkouts
            .Include(c => c.Worker)  // Assuming a Worker relationship, adjust as needed
            .Include(c => c.Game)    // Assuming a Game relationship, adjust as needed
            .Include(c => c.GameConsole) // Assuming a Console relationship, adjust as needed
            .ToListAsync();
    }

    // Get a checkout by its ID
    public async Task<Checkout?> GetCheckoutByIdAsync(int id)
    {
        return await _context.Checkouts
            .Include(c => c.Worker)  // Assuming a Worker relationship, adjust as needed
            .Include(c => c.Game)    // Assuming a Game relationship, adjust as needed
            .Include(c => c.GameConsole) // Assuming a Console relationship, adjust as needed
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // Add a new checkout
    public async Task AddCheckoutAsync(Checkout checkout)
    {
        await _context.Checkouts.AddAsync(checkout);
        await _context.SaveChangesAsync();
    }

}