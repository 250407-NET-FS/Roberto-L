using GameTracker.Models;
using GameTracker.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Repositories;
public interface ITagRepository
{
    Task AddTagAsync(Tag tag);
    Task<Tag?> GetTagByNameAsync(string name);
    Task<List<Tag>> GetAllTagsAsync();

    // You already have AddTagAsync(Tag tag), but you can overload if needed:
    Task<Tag> AddTagAsync(TagCreateDTO tagDto);
}
public interface IGameTagRepository
{
    Task AddGameTagAsync(GameTag gameTag);
    Task<List<GameTag>> GetAllGameTagsAsync();
    Task<List<Tag>> GetTagsByGameIdAsync(string gameId);
    Task<List<string>> GetGameIdsByTagNameAsync(string tagName);

    // Add this method to check if a tag is already linked to a game
    Task<GameTag?> GetByGameIdAndTagIdAsync(int gameId, int tagId);

    Task SaveChangesAsync();
}