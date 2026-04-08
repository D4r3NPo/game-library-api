using GameLibrary.Shared.DTOs;
using GameLibrary.Shared.Enums;
using GameLibraryApi.Data;
using GameLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(AppDbContext db) : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(typeof(GameResponse), StatusCodes.Status201Created)]
	public async Task<IActionResult> Create([FromBody] CreateGameRequest dto)
	{
		var newGame = new Game
		{
			Id = Guid.NewGuid().ToString(),
			Title = dto.Title,
			Platforms = dto.Platforms,
			Status = dto.Status,
			Rating = dto.Rating,
		};
		db.Games.Add(newGame);
		await db.SaveChangesAsync();
		return CreatedAtAction(nameof(GetById), new { id = newGame.Id }, newGame.ToGameResponseDto());
	}

	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(string id)
	{
		var game = await db.Games.FindAsync(id);
		return game is null ? NotFound() : Ok(game.ToGameResponseDto());
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<GameResponse>), StatusCodes.Status200OK)]
	public async Task<IActionResult> Get([FromQuery] GameStatus? status, [FromQuery] GamePlatform? platform, [FromQuery] string? title)
	{
		IQueryable<Game> query = db.Games;

		if (status is not null)
			query = query.Where(g => g.Status == status);

		if (platform is not null)
			query = query.Where(g => g.Platforms.Contains(platform.Value));

		if (!string.IsNullOrWhiteSpace(title))
			query = query.Where(g => g.Title.Contains(title));

		List<GameResponse> games = await query.Select(g => g.ToGameResponseDto()).ToListAsync();
		return Ok(games);
	}

	[HttpPut("{id}")]
	[ProducesResponseType(typeof(GameResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(string id, [FromBody] UpdateGameRequest dto)
	{
		Game? existingGame = await db.Games.FindAsync(id);
		if (existingGame is null) return NotFound();

		existingGame.Title = dto.Title;
		existingGame.Platforms = dto.Platforms;
		existingGame.Status = dto.Status;
		existingGame.Rating = dto.Rating;

		await db.SaveChangesAsync();

		return Ok(existingGame.ToGameResponseDto());
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(string id)
	{
		var game = await db.Games.FindAsync(id);
		if (game is null) return NotFound();

		db.Games.Remove(game);

		await db.SaveChangesAsync();

		return NoContent();
	}
}
