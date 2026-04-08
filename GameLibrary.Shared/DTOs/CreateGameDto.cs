using System.ComponentModel.DataAnnotations;
using GameLibrary.Shared.Enums;

namespace GameLibrary.Shared.DTOs;

public class CreateGameRequest
{
    [Required, Length(1, 30)]
    public string Title { get; set; } = "";
    public GamePlatform[] Platforms { get; set; } = [];
    public GameStatus Status { get; set; } = GameStatus.ToDo;
    [Range(0, 10)]
    public int? Rating { get; set; }
}