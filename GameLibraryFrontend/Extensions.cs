using GameLibrary.Shared.DTOs;

namespace GameLibraryFrontend;

public static class Extensions
{
    public static UpdateGameRequest ToUpdateGameRequest(this GameResponse game) => new()
    {
        Title = game.Title,
        Platforms = game.Platforms,
        Status = game.Status,
        Rating = game.Rating,
    };

    public static GameResponse Clone(this GameResponse game) => new()
    {
        Title = game.Title,
        Platforms = game.Platforms,
        Status = game.Status,
        Rating = game.Rating,
    };
}