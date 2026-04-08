using GameLibrary.Shared.DTOs;

namespace GameLibraryFrontend;

public static class Extensions
{
    public static UpdateGameDto ToUpdateGameDto(this GameResponseDto dto) => new()
    {
        Title = dto.Title,
        Platforms = dto.Platforms,
        Genres = dto.Genres,
        Status = dto.Status,
        Rating = dto.Rating,
    };
}