namespace GameLibrary.Models;

public class Game
{
    public string Id { get; init; } = "";
    public string Title { get; set; } = "";
    public Platform[] Platforms { get; set; } = [];
    public Genre[] Genres { get; set; } = [];
    public Status Status { get; set; } = Status.ToDo;
    public int? Rating { get; set; } = null;
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