using GameLibraryApi.Models;

namespace GameLibraryApi.DTOs;

public class UpdateGameDto
{
    public string Title { get; set; } = "";
    public Platform[] Platforms { get; set; } = [];
    public Genre[] Genres { get; set; } = [];
    public Status Status { get; set; }
    public int? Rating { get; set; }
}