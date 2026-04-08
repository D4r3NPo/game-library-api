using GameLibrary.Shared.DTOs;

namespace GameLibraryApi.Models
{
    public static class Extensions
    {
        public static GameResponse ToGameResponseDto(this Game game) => new()
        {
            Id = game.Id,
            Title = game.Title,
            Platforms = game.Platforms,
            Status = game.Status,
            Rating = game.Rating,
        };
    }
}