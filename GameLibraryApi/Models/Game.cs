namespace GameLibraryApi.Models;
using System.ComponentModel.DataAnnotations;

public class Game
{
    [Required]
    public string Id { get; init; } = "";
    [Required, Length(1, 30)]
    public string Title { get; set; } = "";
    public Platform[] Platforms { get; set; } = [];
    public Genre[] Genres { get; set; } = [];
    public Status Status { get; set; } = Status.ToDo;
    [Range(0, 10)]
    public int? Rating { get; set; } = null;

    public override bool Equals(object? obj)
    {
        return obj is Game game &&
            Id == game.Id &&
            Title == game.Title &&
            Platforms.SequenceEqual(game.Platforms) &&
            Genres.SequenceEqual(game.Genres) &&
            Status == game.Status &&
            Rating == game.Rating;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Title, Platforms, Genres, Status, Rating);
}

public enum Platform
{
    MacOS,
    Linux,
    Windows,
    iOS,
    Android,
}

public enum Status
{
    ToDo,
    InProgress,
    Done
}

public enum Genre
{
    RPG,
    Factory,
    Sandbox,
    OpenWorld,
    Dungeon,
    Multiplayer,
    FPS
}