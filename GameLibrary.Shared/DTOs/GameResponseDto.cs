using GameLibrary.Shared.Enums;

namespace GameLibrary.Shared.DTOs;

public class GameResponse
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public GamePlatform[] Platforms { get; set; } = [];
    public GameStatus Status { get; set; } = GameStatus.ToDo;
    public int? Rating { get; set; } = null;

    public GameResponse() { }
}