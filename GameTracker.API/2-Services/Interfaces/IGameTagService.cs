using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Services;

public interface ITagService
{
    Task<TagDTO> AddTagAsync(TagCreateDTO dto);
    Task<List<TagDTO>> GetAllTagsAsync();
}

public interface IGameTagService
{
    Task AddGameTagsAsync(GameTagCreateDTO dto);
    Task<List<Game>> GetGamesByTagAsync(string tagName);

    Task<List<TagDTO>> GetTagsForGameAsync(int gameId);
}