using GameTracker.DTOs;
using GameTracker.Models;
using GameTracker.Repositories;
using GameTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Register your DbContext with the DI container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GameTrackerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameConsoleService, GameConsoleService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IGameTagService, GameTagService>();

// Register repositories
builder.Services.AddScoped<IGameRepository, EfGameRepository>();
builder.Services.AddScoped<IGameConsoleRepository, EfGameConsoleRepository>();
builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();
builder.Services.AddScoped<IWorkerRepository, EfWorkerRepository>();
builder.Services.AddScoped<ICheckoutRepository, EfCheckoutRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<IGameTagRepository, EfGameTagRepository>();


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

// Get all Stores
app.MapGet("/stores", async (IStoreService service) =>
{
    try
    {
        var stores = await service.GetAllStoresAsync();
        return Results.Ok(stores);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get Store by Id
app.MapGet("/stores/{id}", async (IStoreService service, int id) =>
{
    var store = await service.GetStoreByIdAsync(id);
    return store != null ? Results.Ok(store) : Results.NotFound($"Store with ID {id} not found.");
});

// Add Store
app.MapPost("/stores", async (StoreDTO storeCreateDTO, IStoreService storeService) =>
{
    if (storeCreateDTO == null || string.IsNullOrWhiteSpace(storeCreateDTO.Name))
        return Results.BadRequest("Store name is required.");

    try
    {
        var store = await storeService.AddStoreAsync(storeCreateDTO);
        return Results.Created($"/stores/{store.Id}", store);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Update Store
app.MapPut("/stores/{id}", async (int id, StoreDTO storeDTO, IStoreService storeService) =>
{
    var store = await storeService.GetStoreByIdAsync(id);
    if (store == null)
        return Results.NotFound("Store not found.");

    if (string.IsNullOrWhiteSpace(storeDTO.Name))
        return Results.BadRequest("Store name cannot be empty.");

    storeDTO.Id = id;
    await storeService.UpdateStoreAsync(storeDTO);
    return Results.Ok(storeDTO);
});

// Get all Workers
app.MapGet("/workers", async (IWorkerService service) =>
{
    try
    {
        var workers = await service.GetAllWorkersAsync();
        return Results.Ok(workers);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get Worker by ID
app.MapGet("/workers/by-id/{id}", async (IWorkerService service, int id) =>
{
    var worker = await service.GetWorkerByIdAsync(id);
    return worker != null ? Results.Ok(worker) : Results.NotFound($"Worker with ID {id} not found.");
});

// Add Worker
app.MapPost("/workers", async (WorkerCreateDTO workerDTO, IWorkerService workerService) =>
{
    if (workerDTO == null || string.IsNullOrWhiteSpace(workerDTO.Name) || string.IsNullOrWhiteSpace(workerDTO.Username))
        return Results.BadRequest("Worker name and username are required.");

    try
    {
        var addedWorker = await workerService.AddWorkerAsync(workerDTO); // Use workerDTO here
        return Results.Created($"/workers/{addedWorker.Username}", addedWorker);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get all GameConsoles
app.MapGet("/gameConsoles", async (IGameConsoleService service) =>
{
    try
    {
        var consoles = await service.GetAllGameConsolesAsync();
        return Results.Ok(consoles);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get GameConsole by Id
app.MapGet("/gameConsoles/{id}", async (IGameConsoleService service, int id) =>
{
    var console = await service.GetGameConsoleByIdAsync(id);
    return console != null ? Results.Ok(console) : Results.NotFound($"Game Console with ID {id} not found.");
});

// Add Game Console
app.MapPost("/gameConsoles", async (GameConsoleDTO dto, IGameConsoleService service) =>
{
    if (dto == null)
        return Results.BadRequest("Game console data is required.");

    try
    {
        await service.AddGameConsoleAsync(dto);
        return Results.Created($"/gameConsoles/{dto.Id}", dto);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get all Games
app.MapGet("/games", async (IGameService service) =>
{
    try
    {
        var games = await service.GetAllGamesAsync();
        return Results.Ok(games);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get Game by Id
app.MapGet("/games/{id}", async (IGameService service, int id) =>
{
    var game = await service.GetGameByIdAsync(id);
    return game != null ? Results.Ok(game) : Results.NotFound($"Game with ID {id} not found.");
});

// Add Game
app.MapPost("/games", async (GameDTO gameCreateDTO, IGameService gameService) =>
{
    if (gameCreateDTO == null || string.IsNullOrWhiteSpace(gameCreateDTO.Name))
        return Results.BadRequest("Game name is required.");

    try
    {
        await gameService.AddGameAsync(gameCreateDTO);
        return Results.Created($"/games/{gameCreateDTO.Id}", gameCreateDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get all Tags
app.MapGet("/tags", async (ITagService service) =>
{
    var tags = await service.GetAllTagsAsync();
    return Results.Ok(tags);
});

// Add a new Tag
app.MapPost("/tags", async (TagCreateDTO dto, ITagService service) =>
{
    try
    {
        var tag = await service.AddTagAsync(dto);
        return Results.Created("/tags", tag);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message, statusCode: 400);
    }
});

// Search Games via Tags
app.MapGet("/games/by-tag/{tagName}", async (string tagName, IGameTagService tagService) =>
{
    try
    {
        var games = await tagService.GetGamesByTagAsync(tagName);
        if (games == null || games.Count == 0)
            return Results.NotFound("No games found with this tag.");

        return Results.Ok(games);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get all Checkouts
app.MapGet("/checkouts", async (ICheckoutService service) =>
{
    var checkouts = await service.GetAllCheckoutsAsync();
    return Results.Ok(checkouts);
});

// Get Checkout by Id
app.MapGet("/checkouts/{id}", async (ICheckoutService service, int id) =>
{
    var result = await service.GetCheckoutByIdAsync(id);
    return result != null ? Results.Ok(result) : Results.NotFound();
});

// Add a Checkout
app.MapPost("/checkouts", async (CheckoutCreateDTO dto, ICheckoutService checkoutService) =>
{
    try
    {
        var checkout = await checkoutService.AddCheckoutAsync(dto);
        return Results.Created($"/checkouts/{checkout.Id}", checkout);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get all tags for a game by gameId
app.MapGet("/game-tags/{gameId}", async (int gameId, IGameTagService service) =>
{
    try
    {
        var gameTagDTO = await service.GetTagsForGameAsync(gameId);
        return gameTagDTO != null
            ? Results.Ok(gameTagDTO)
            : Results.NotFound($"No tags found for Game ID {gameId}.");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Add tags to a game
app.MapPost("/game-tags", async (GameTagCreateDTO dto, IGameTagService service) =>
{
    if (dto == null || dto.GameId <= 0 || dto.TagNames == null || !dto.TagNames.Any())
        return Results.BadRequest("GameId and a list of tag names are required.");

    try
    {
        await service.AddGameTagsAsync(dto);
        return Results.Ok("Tags added to game.");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.Run();