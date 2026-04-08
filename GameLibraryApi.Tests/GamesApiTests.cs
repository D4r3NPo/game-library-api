using System.Net;
using System.Net.Http.Json;
using GameLibrary.Shared.DTOs;
using GameLibrary.Shared.Enums;

namespace GameLibraryApi.Tests;

public class GamesApiTests
{
    private static CustomWebApplicationFactory CreateFactory() => new();

    [Fact]
    public async Task Get_Games_Returns_Ok()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/games");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Then_GetById_Works()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var gameCreationRequest = new CreateGameRequest
        {
            Title = "Celeste",
            Platforms = [],
            Status = GameStatus.ToDo,
            Rating = 10
        };

        var postResponse = await client.PostAsJsonAsync("/api/games", gameCreationRequest);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var gameCreationResponse = await postResponse.Content.ReadFromJsonAsync<GameResponse>();
        Assert.NotNull(gameCreationResponse);

        var getResponse = await client.GetAsync($"/api/games/{gameCreationResponse.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var createdGame = await getResponse.Content.ReadFromJsonAsync<GameResponse>();

        Assert.NotNull(gameCreationResponse);
        Assert.Equal("Celeste", gameCreationResponse.Title);
    }

    [Fact]
    public async Task Get_Unknown_Game_Returns_NotFound()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/games/unknown");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_Then_Get_Works()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var initialGame = new CreateGameRequest
        {
            Title = "Celeste",
            Platforms = [],
            Status = GameStatus.ToDo,
            Rating = 10
        };

        var createResponse = await client.PostAsJsonAsync("/api/games", initialGame);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdGame = await createResponse.Content.ReadFromJsonAsync<GameResponse>();
        Assert.NotNull(createdGame);

        var updatedGame = new UpdateGameRequest
        {
            Title = "Celeste",
            Platforms = [],
            Status = GameStatus.ToDo,
            Rating = 2
        };

        var updateGameResponse = await client.PutAsJsonAsync($"/api/games/{createdGame.Id}", updatedGame);
        Assert.Equal(HttpStatusCode.OK, updateGameResponse.StatusCode);

        var getUpdatedGameResponse = await client.GetAsync($"/api/games/{createdGame.Id}");
        Assert.Equal(HttpStatusCode.OK, getUpdatedGameResponse.StatusCode);

        var retrievedUpdatedGame = await getUpdatedGameResponse.Content.ReadFromJsonAsync<GameResponse>();

        Assert.NotNull(retrievedUpdatedGame);
        Assert.Equal(2, retrievedUpdatedGame.Rating);
    }

    [Fact]
    public async Task Create_With_Invalid_Title_Returns_BadRequest()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var game = new CreateGameRequest
        {
            Title = "",
            Platforms = [],
            Status = GameStatus.ToDo,
            Rating = 5
        };

        var response = await client.PostAsJsonAsync("/api/games", game);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_With_Invalid_Rating_Returns_BadRequest()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var game = new CreateGameRequest
        {
            Title = "Invalid Game",
            Platforms = [],
            Status = GameStatus.ToDo,
            Rating = 15
        };

        var response = await client.PostAsJsonAsync("/api/games", game);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Get_With_Filters_Works()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var game1 = new CreateGameRequest
        {
            Title = "Game 1",
            Platforms = [GamePlatform.Windows],
            Status = GameStatus.ToDo,
            Rating = 5
        };

        var game2 = new CreateGameRequest
        {
            Title = "Game 2",
            Platforms = [GamePlatform.Linux, GamePlatform.Windows],
            Status = GameStatus.InProgress,
            Rating = 7
        };

        await client.PostAsJsonAsync("/api/games", game1);
        await client.PostAsJsonAsync("/api/games", game2);

        var response = await client.GetAsync("/api/games?status=ToDo&platform=Windows");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var games = await response.Content.ReadFromJsonAsync<List<GameResponse>>();
        Assert.NotNull(games);
        Assert.Single(games);
        Assert.Equal(game1.Title, games[0].Title);

        response = await client.GetAsync("/api/games?platform=Windows");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        games = await response.Content.ReadFromJsonAsync<List<GameResponse>>();
        Assert.NotNull(games);
        Assert.Equal(2, games.Count);

        response = await client.GetAsync("/api/games?title=Game");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        games = await response.Content.ReadFromJsonAsync<List<GameResponse>>();
        Assert.NotNull(games);
        Assert.Equal(2, games.Count);
    }

    [Fact]
    public async Task Delete_Works()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var game1 = new CreateGameRequest
        {
            Title = "Game 1",
            Platforms = [GamePlatform.Windows],
            Status = GameStatus.ToDo,
            Rating = 5
        };

        var game2 = new CreateGameRequest
        {
            Title = "Game 2",
            Platforms = [GamePlatform.Linux, GamePlatform.Windows],
            Status = GameStatus.InProgress,
            Rating = 7
        };

        await client.PostAsJsonAsync("/api/games", game1);
        var postResponse = await client.PostAsJsonAsync("/api/games", game2);
        var game2Id = (await postResponse.Content.ReadFromJsonAsync<GameResponse>())?.Id;
        Assert.NotNull(game2Id);

        var gameCountBefore = await GetGameCount();
        Assert.Equal(2, gameCountBefore);

        var deleteResponse = await client.DeleteAsync($"/api/games/{game2Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var gameCountAfter = await GetGameCount();
        Assert.Equal(1, gameCountAfter);

        async Task<int> GetGameCount()
        {
            var response = await client.GetAsync("/api/games");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var games = await response.Content.ReadFromJsonAsync<List<GameResponse>>();
            Assert.NotNull(games);
            return games.Count;
        }
    }


    [Fact]
    public async Task Update_Invalid_Id_Fails()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

         var updatedGame = new UpdateGameRequest
        {
            Title = "Celeste",
             Platforms = [],
             Status = GameStatus.ToDo,
            Rating = 2
        };

        var updateResponse = await client.PutAsJsonAsync($"/api/games/invalid-id", updatedGame);
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Invalid_Id_Fails()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var deleteResponse = await client.DeleteAsync($"/api/games/invalid-id");
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

}