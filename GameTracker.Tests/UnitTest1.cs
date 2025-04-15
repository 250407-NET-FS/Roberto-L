using Xunit;
using Moq;
using GameTracker.Services;
using GameTracker.Repositories;
using GameTracker.Models;
using GameTracker.DTOs;
using System.Collections.Generic;

namespace GameTracker.Tests;

public class GameServiceTests
{
    [Fact]
    public void AddOrUpdateGame_ShouldAddNewGame_WhenGameDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.GetAllGames()).Returns(new List<Game>());

        var gameService = new GameService(mockRepo.Object);

        var newGameDto = new GameCreateDTO
        {
            Store_Id = "store001",
            Inventory = 5,
            Price = 59.99m,
            Condition = GameCondition.New,
            Publisher = "Nintendo",
            Developer = "Nintendo EPD",
            Name = "The Legend of Zelda",
            Year = 2023
        };

        // Act
        gameService.AddOrUpdateGame(newGameDto);

        // Assert
        mockRepo.Verify(r => r.AddGame(It.IsAny<Game>()), Times.Once);
        mockRepo.Verify(r => r.UpdateGame(It.IsAny<Game>()), Times.Never);
    }
}
