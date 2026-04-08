using GameLibrary.Shared.Enums;

namespace GameLibraryApi.Models;

public class Game
{
	public string Id { get; set; } = "";
	public string Title { get; set; } = "";
	public GamePlatform[] Platforms { get; set; } = [];
	public GameStatus Status { get; set; } = GameStatus.ToDo;
	public int? Rating { get; set; } = null;

	public override bool Equals(object? obj)
	{
		return obj is Game game &&
			Id == game.Id &&
			Title == game.Title &&
			Platforms.SequenceEqual(game.Platforms) &&
			Status == game.Status &&
			Rating == game.Rating;
	}

	public override int GetHashCode() => HashCode.Combine(Id, Title, Platforms, Status, Rating);
}
