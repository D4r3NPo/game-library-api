using GameLibrary.Shared.DTOs;

namespace GameLibraryFrontend.Services;

public class GameClient(HttpClient http)
{
    private readonly HttpClient _http = http;

    public async Task<GameResponse?> CreateGameAsync(CreateGameRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/games", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameResponse>();
    }

    public async Task<List<GameResponse>?> GetGamesAsync()
    {
        return await _http.GetFromJsonAsync<List<GameResponse>>("api/games");
    }

    public async Task<GameResponse?> UpdateAsync(string id, UpdateGameRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/games/{id}", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameResponse>();
    }    

    public async Task DeleteAsync(string id)
    {
        var response = await _http.DeleteAsync($"api/games/{id}");
        response.EnsureSuccessStatusCode();
    }
}