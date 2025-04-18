using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Services;
public class TagService : ITagService
{
    private readonly GameTrackerDbContext _context;

    public TagService(GameTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<TagDTO> AddTagAsync(TagCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new Exception("Tag name cannot be empty.");

        var existing = await _context.Tags.FirstOrDefaultAsync(t => t.Name == dto.Name);
        if (existing != null)
            throw new Exception("Tag with this name already exists.");

        var tag = new Tag { Name = dto.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return new TagDTO
        {
            TagId = tag.TagId,
            Name = tag.Name
        };
    }

    public async Task<List<TagDTO>> GetAllTagsAsync()
    {
        return await _context.Tags
            .Select(t => new TagDTO { TagId = t.TagId, Name = t.Name })
            .ToListAsync();
    }
}
public class GameTagService : IGameTagService
{
    private readonly GameTrackerDbContext _context;
    private readonly IGameRepository _gameRepo;
    private readonly ITagRepository _tagRepo;
    private readonly IGameTagRepository _gameTagRepo;

    public GameTagService(GameTrackerDbContext context, IGameRepository gameRepo, ITagRepository tagRepo, IGameTagRepository gameTagRepo)
    {
        _context = context;
        _gameRepo = gameRepo;
        _tagRepo = tagRepo;
        _gameTagRepo = gameTagRepo;
    }

    public async Task<List<Game>> GetGamesByTagAsync(string tagName)
    {
        return await _context.GameTags
            .Where(gt => gt.Tag.Name == tagName)
            .Select(gt => gt.Game)
            .Distinct()
            .ToListAsync();
    }

    public async Task AddTagsToGameAsync(int gameId, List<string> tagNames)
    {
        var game = await _gameRepo.GetGameByIdAsync(gameId);
        if (game == null)
            throw new ArgumentException("Game not found.");

        foreach (var tagName in tagNames.Distinct())
        {
            var tag = await _tagRepo.GetTagByNameAsync(tagName) ??
                      await _tagRepo.AddTagAsync(new TagCreateDTO { Name = tagName });

            var existingLink = await _gameTagRepo.GetByGameIdAndTagIdAsync(gameId, tag.TagId);
            if (existingLink == null)
            {
                await _gameTagRepo.AddGameTagAsync(new GameTag
                {
                    GameId = gameId,
                    TagId = tag.TagId
                });
            }
        }

        await _gameTagRepo.SaveChangesAsync();
    }

    public async Task AddGameTagsAsync(GameTagCreateDTO dto)
    {
        var game = await _gameRepo.GetGameByIdAsync(dto.GameId);
        if (game == null)
            throw new ArgumentException("Game not found.");

        foreach (var tagName in dto.TagNames.Distinct())
        {
            var tag = await _tagRepo.GetTagByNameAsync(tagName)
                      ?? await _tagRepo.AddTagAsync(new TagCreateDTO { Name = tagName });

            var existingGameIds = await _gameTagRepo.GetGameIdsByTagNameAsync(tag.Name);
            if (!existingGameIds.Contains(game.Id.ToString()))
            {
                await _gameTagRepo.AddGameTagAsync(new GameTag
                {
                    GameId = game.Id,
                    TagId = tag.TagId
                });
            }
        }

        await _gameTagRepo.SaveChangesAsync();
    }
    public async Task<List<TagDTO>> GetTagsForGameAsync(int gameId)
    {
        var tags = await _gameTagRepo.GetTagsByGameIdAsync(gameId.ToString());
        return tags.Select(t => new TagDTO
        {
            TagId = t.TagId,
            Name = t.Name
        }).ToList();
    }
}