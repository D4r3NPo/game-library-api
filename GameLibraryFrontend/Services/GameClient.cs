using GameLibrary.Shared.DTOs;

namespace GameLibraryFrontend.Services;

public class GameClient(HttpClient http)
{
    private readonly HttpClient _http = http;

    public async Task<List<GameResponseDto>?> GetGamesAsync()
    {
        return await _http.GetFromJsonAsync<List<GameResponseDto>>("api/games");
    }

    public async Task<GameResponseDto?> CreateGameAsync(CreateGameDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/games", dto);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<GameResponseDto>();
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _http.DeleteAsync($"api/games/{id}");
        response.EnsureSuccessStatusCode();
    }
}