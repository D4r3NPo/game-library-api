using GameLibrary.Shared.DTOs;

namespace GameLibraryApi.Models
{
    public static class Extensions
    {
        public static GameResponseDto ToGameResponseDto(this Game game) => new()
        {
            Id = game.Id,
            Title = game.Title,
            Platforms = game.Platforms,
            Genres = game.Genres,
            Status = game.Status,
            Rating = game.Rating
        };
    }
}