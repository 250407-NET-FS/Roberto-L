using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using GameTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IGameService, GameService>(); // Register GameService

// registor Repositoriies
builder.Services.AddSingleton<IGameRepository, JsonGameRepository>(); // Register GameRepository (JSON-based)

builder.Services.AddControllers(); // Register controllers


//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "GameTracker";
    config.Title = "GameTracker v0.2";
    config.Version = "v0.2";
    config.Description = "API for managing game and console inventory, checkouts, and more.";
});

var app = builder.Build();

// Enable Swagger in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(); // Serve OpenAPI specification (swagger.json)
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "GameTracker API";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list"; // Optional: Adjust swagger UI view
    });
}

app.MapGet(
    "/games",
    (IGameRepository repo) =>
    {
        try
        {
            return Results.Ok(repo.GetAllGames());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
);

app.MapPost("/games", async (GameCreateDTO gameCreateDTO, IGameService gameService) =>
{
    if (gameCreateDTO == null)
        return Results.BadRequest("Invalid game data.");

    // Create a game using the service
    gameService.AddOrUpdateGame(gameCreateDTO);
    return Results.Created($"/api/games/{gameCreateDTO.Name}", gameCreateDTO);
});

app.Run();
