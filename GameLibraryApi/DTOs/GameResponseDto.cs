using GameLibraryApi.Models;

namespace GameLibraryApi.DTOs;

public class GameResponseDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public Platform[] Platforms { get; set; }  = [];
    public Genre[] Genres { get; set; } = [];
    public Status Status { get; set; } = Status.ToDo;
    public int? Rating { get; set; } = null;

    public GameResponseDto() { }


    public GameResponseDto(Game game)
    {
        Id = game.Id;
        Title = game.Title;
        Platforms = game.Platforms;
        Genres = game.Genres;
        Status = game.Status;
        Rating = game.Rating;
    }
}