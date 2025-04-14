using GameTracker.DTOs;
using GameTracker.Models;

namespace GameTracker.Repositories;

public interface IGameConsoleService
{
    void AddOrUpdateGame(GameConsole incomingGameConsole);
}