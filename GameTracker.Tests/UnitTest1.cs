using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameTracker.Services;
using GameTracker.Models;
using GameTracker.DTOs;
using GameTracker.Repositories;
using System.Linq;
using System;
using GameTracker;

public class GameServiceTests
{
    private GameTrackerDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<GameTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GameTrackerDbContext(options);
    }

    [Fact]
    public async Task AddGameAsync_ValidInput_AddsGame()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();
        var service = new GameService(dbContext, repoMock.Object);

        var gameDTO = new GameDTO
        {
            Name = "Test Game",
            StoreId = 1,
            Price = 49.99m,
            Condition = "New",
            Publisher = "Test Publisher",
            Developer = "Test Dev",
            Year = 2024
        };

        var result = await service.AddGameAsync(gameDTO);

        Assert.NotNull(result);
        Assert.Equal(gameDTO.Name, result.Name);
        Assert.Equal(GameCondition.New, result.Condition);
        Assert.Single(dbContext.Games);
    }

    [Fact]
    public async Task AddGameAsync_MissingName_ThrowsException()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();
        var service = new GameService(dbContext, repoMock.Object);

        var gameDTO = new GameDTO
        {
            Name = "", // Invalid
            StoreId = 1,
            Price = 29.99m,
            Condition = "Used",
            Publisher = "Test Publisher",
            Developer = "Test Dev",
            Year = 2023
        };

        await Assert.ThrowsAsync<Exception>(() => service.AddGameAsync(gameDTO));
    }

    [Fact]
    public async Task GetAllGamesAsync_ReturnsGames()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();

        dbContext.Games.Add(new Game
        {
            Name = "Halo",
            StoreId = 1,
            Price = 59.99m,
            Condition = GameCondition.New,
            Publisher = "Microsoft",
            Developer = "Bungie",
            Year = 2001
        });
        await dbContext.SaveChangesAsync();

        var service = new GameService(dbContext, repoMock.Object);
        var games = await service.GetAllGamesAsync();

        Assert.Single(games);
        Assert.Equal("Halo", games[0].Name);
    }

    [Fact]
    public async Task GetGameByIdAsync_GameExists_ReturnsGame()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();
        var game = new Game
        {
            Name = "Elden Ring",
            StoreId = 2,
            Price = 59.99m,
            Condition = GameCondition.New,
            Publisher = "FromSoftware",
            Developer = "FromSoftware",
            Year = 2022
        };
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();

        var service = new GameService(dbContext, repoMock.Object);
        var result = await service.GetGameByIdAsync(game.Id);

        Assert.NotNull(result);
        Assert.Equal("Elden Ring", result.Name);
    }

    [Fact]
    public async Task GetGameByIdAsync_GameDoesNotExist_ReturnsNull()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();
        var service = new GameService(dbContext, repoMock.Object);

        var result = await service.GetGameByIdAsync(999); // Non-existent ID

        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveGameAsync_GameExists_RemovesGame()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();

        var game = new Game
        {
            Name = "Delete Me",
            StoreId = 1,
            Price = 9.99m,
            Condition = GameCondition.Damaged,
            Publisher = "Unknown",
            Developer = "Unknown",
            Year = 2010
        };
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();

        var service = new GameService(dbContext, repoMock.Object);
        await service.RemoveGameAsync(game.Id);

        Assert.Empty(dbContext.Games);
    }

    [Fact]
    public async Task GetGamesByFiltersAsync_FilterByYear_ReturnsCorrectGames()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();

        dbContext.Games.AddRange(
    new Game
    {
        Name = "Game A",
        StoreId = 1,
        Year = 2021,
        Condition = GameCondition.New,
        Developer = "Dev A",
        Publisher = "Pub A",
        Price = 29.99m
    },
    new Game
    {
        Name = "Game B",
        StoreId = 1,
        Year = 2022,
        Condition = GameCondition.Used,
        Developer = "Dev B",
        Publisher = "Pub B",
        Price = 19.99m
    }
);
        await dbContext.SaveChangesAsync();

        var service = new GameService(dbContext, repoMock.Object);

        var filters = new Dictionary<string, object> { { "year", 2021 } };
        var results = await service.GetGamesByFiltersAsync(filters);

        Assert.Single(results);
        Assert.Equal("Game A", results[0].Name);
    }

    [Fact]
    public async Task GetGamesByFiltersAsync_InvalidCondition_IgnoresFilter()
    {
        var dbContext = GetInMemoryDbContext();
        var repoMock = new Mock<IGameRepository>();

        dbContext.Games.Add(new Game
        {
            Name = "Filtered Game",
            StoreId = 1,
            Year = 2020,
            Condition = GameCondition.New,
            Developer = "Some Dev",
            Publisher = "Some Pub",
            Price = 39.99m
        });
        await dbContext.SaveChangesAsync();

        var service = new GameService(dbContext, repoMock.Object);

        var filters = new Dictionary<string, object> { { "condition", "NonExistentCondition" } };
        var results = await service.GetGamesByFiltersAsync(filters);

        // Filter should be ignored; the game still appears
        Assert.Single(results);
    }
}

