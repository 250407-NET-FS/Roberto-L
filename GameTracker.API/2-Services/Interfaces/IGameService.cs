using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameService
{
    void AddOrUpdateGame(Game incomingGame);
}