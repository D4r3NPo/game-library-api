using GameLibraryApi.Data;
using GameLibraryApi.DTOs;
using GameLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(AppDbContext db) : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status201Created)]
	public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
	{
		var newGame = new Game
		{
			Id = Guid.NewGuid().ToString(),
			Title = dto.Title,
			Platforms = dto.Platforms,
			Genres = dto.Genres,
			Status = dto.Status,
			Rating = dto.Rating,
		};
		db.Games.Add(newGame);
		await db.SaveChangesAsync();
		return CreatedAtAction(nameof(GetById), new { id = newGame.Id }, new GameResponseDto(newGame));
	}

	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById(string id)
	{
		var game = await db.Games.FindAsync(id);
		return game is null ? NotFound() : Ok(new GameResponseDto(game));
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<GameResponseDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> Get([FromQuery] Status? status, [FromQuery] Platform? platform, [FromQuery] string? title)
	{
		IQueryable<Game> query = db.Games;

		if (status is not null)
			query = query.Where(g => g.Status == status);

		if (platform is not null)
			query = query.Where(g => g.Platforms.Contains(platform.Value));

		if (!string.IsNullOrWhiteSpace(title))
			query = query.Where(g => g.Title.Contains(title));

		List<GameResponseDto> games = await query.Select(g => new GameResponseDto(g)).ToListAsync();
		return Ok(games);
	}

	[HttpPut("{id}")]
	[ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(string id, [FromBody] UpdateGameDto dto)
	{
		Game? existingGame = await db.Games.FindAsync(id);
		if (existingGame is null) return NotFound();

		existingGame.Title = dto.Title;
		existingGame.Platforms = dto.Platforms;
		existingGame.Genres = dto.Genres;
		existingGame.Status = dto.Status;
		existingGame.Rating = dto.Rating;

		await db.SaveChangesAsync();

		return Ok(new GameResponseDto(existingGame));
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
