using Xunit;
using Moq;
using GameTracker.Services;
using GameTracker.Repositories;
using GameTracker.Models;
using GameTracker.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Tests
{
    public class GameServiceTests
    {
        private Mock<GameTrackerDbContext> CreateMockDbContext(List<Game> games)
        {
            var mockDbSet = new Mock<DbSet<Game>>();
            mockDbSet.As<IQueryable<Game>>().Setup(m => m.Provider).Returns(games.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Game>>().Setup(m => m.Expression).Returns(games.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Game>>().Setup(m => m.ElementType).Returns(games.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Game>>().Setup(m => m.GetEnumerator()).Returns(games.GetEnumerator());

            var mockContext = new Mock<GameTrackerDbContext>();
            mockContext.Setup(c => c.Games).Returns(mockDbSet.Object);
            return mockContext;
        }

        [Fact]
        public async Task GetAllGamesAsync_ShouldReturnGames()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game { Id = 1, StoreId = 1, Name = "Game1", Price = 10.99m, Condition = GameCondition.New, Publisher = "Publisher1", Developer = "Developer1", Year = 2023 },
                new Game { Id = 2, StoreId = 1, Name = "Game2", Price = 15.99m, Condition = GameCondition.Used, Publisher = "Publisher2", Developer = "Developer2", Year = 2022 }
            };
            var mockContext = CreateMockDbContext(games);
            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            // Act
            var result = await gameService.GetAllGamesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Name == "Game1");
            Assert.Contains(result, g => g.Name == "Game2");
        }

        [Fact]
        public async Task GetGamesByStoreIdAsync_ShouldReturnFilteredGames()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game { Id = 1, StoreId = 1, Name = "Game1", Price = 10.99m, Condition = GameCondition.New, Publisher = "Publisher1", Developer = "Developer1", Year = 2023 },
                new Game { Id = 2, StoreId = 2, Name = "Game2", Price = 15.99m, Condition = GameCondition.Used, Publisher = "Publisher2", Developer = "Developer2", Year = 2022 }
            };
            var mockContext = CreateMockDbContext(games);
            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            // Act
            var result = await gameService.GetGamesByStoreIdAsync(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Game1", result.First().Name);
        }

        [Fact]
        public async Task GetGameByIdAsync_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game { Id = 1, StoreId = 1, Name = "Game1", Price = 10.99m, Condition = GameCondition.New, Publisher = "Publisher1", Developer = "Developer1", Year = 2023 }
            };
            var mockContext = CreateMockDbContext(games);
            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            // Act
            var result = await gameService.GetGameByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Game1", result.Name);
        }

        [Fact]
        public async Task AddGameAsync_ShouldAddNewGame_WhenValidGameDTO()
        {
            // Arrange
            var gameDTO = new GameDTO
            {
                StoreId = 1,
                Name = "New Game",
                Price = 20.99m,
                Condition = GameCondition.New.ToString(),
                Publisher = "Publisher",
                Developer = "Developer",
                Year = 2023
            };

            var mockContext = new Mock<GameTrackerDbContext>();
            mockContext.Setup(c => c.Games.Add(It.IsAny<Game>())).Callback<Game>((game) => game.Id = 1);  // Mock Add method
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(1);  // Mock SaveChangesAsync

            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            // Act
            var result = await gameService.AddGameAsync(gameDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Game", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task RemoveGameAsync_ShouldRemoveGame_WhenGameExists()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "Game1", StoreId = 1 };
            var mockContext = new Mock<GameTrackerDbContext>();
            mockContext.Setup(c => c.Games.FindAsync(1)).ReturnsAsync(game);  // Mock FindAsync
            mockContext.Setup(c => c.Games.Remove(It.IsAny<Game>())).Verifiable();  // Verify Remove

            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            // Act
            await gameService.RemoveGameAsync(1);

            // Assert
            mockContext.Verify(c => c.Games.Remove(It.IsAny<Game>()), Times.Once);  // Check Remove was called
        }

        [Fact]
        public async Task GetGamesByFiltersAsync_ShouldReturnFilteredGames()
        {
            // Arrange
            var games = new List<Game>
            {
                new Game { Id = 1, StoreId = 1, Name = "Game1", Price = 10.99m, Condition = GameCondition.New, Publisher = "Publisher1", Developer = "Developer1", Year = 2023 },
                new Game { Id = 2, StoreId = 2, Name = "Game2", Price = 15.99m, Condition = GameCondition.Used, Publisher = "Publisher2", Developer = "Developer2", Year = 2022 }
            };
            var mockContext = CreateMockDbContext(games);
            var gameService = new GameService(mockContext.Object, Mock.Of<IGameRepository>());

            var filters = new Dictionary<string, object>
            {
                { "storeid", 1 },
                { "condition", GameCondition.New.ToString() }
            };

            // Act
            var result = await gameService.GetGamesByFiltersAsync(filters);

            // Assert
            Assert.Single(result);
            Assert.Equal("Game1", result.First().Name);
        }
    }
}