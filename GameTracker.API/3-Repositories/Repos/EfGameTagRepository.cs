using GameTracker.Models;
using GameTracker.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Repositories;
public class EfTagRepository : ITagRepository
{
    private readonly GameTrackerDbContext _context;

    public EfTagRepository(GameTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddTagAsync(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<Tag?> GetTagByNameAsync(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag> AddTagAsync(TagCreateDTO tagDto)
    {
        var tag = new Tag { Name = tagDto.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return tag;
    }
}

public class EfGameTagRepository : IGameTagRepository
{
    private readonly GameTrackerDbContext _context;

    public EfGameTagRepository(GameTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddGameTagAsync(GameTag gameTag)
    {
        _context.GameTags.Add(gameTag);
    }

    public async Task<List<GameTag>> GetAllGameTagsAsync()
    {
        return await _context.GameTags.Include(gt => gt.Game).Include(gt => gt.Tag).ToListAsync();
    }

    public async Task<List<Tag>> GetTagsByGameIdAsync(string gameId)
    {
        int parsedId = int.Parse(gameId);
        return await _context.GameTags
            .Where(gt => gt.GameId == parsedId)
            .Select(gt => gt.Tag)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetGameIdsByTagNameAsync(string tagName)
    {
        return await _context.GameTags
            .Where(gt => gt.Tag.Name == tagName)
            .Select(gt => gt.GameId.ToString())
            .Distinct()
            .ToListAsync();
    }

    public async Task<GameTag?> GetByGameIdAndTagIdAsync(int gameId, int tagId)
    {
        return await _context.GameTags
            .FirstOrDefaultAsync(gt => gt.GameId == gameId && gt.TagId == tagId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}