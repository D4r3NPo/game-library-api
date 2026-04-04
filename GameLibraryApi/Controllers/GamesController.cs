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
    public async Task<IActionResult> Create([FromBody] Game game)
    {
        db.Games.Add(game);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var game = await db.Games.FindAsync(id);
        return game is null ? NotFound() : Ok(game);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Status? status, [FromQuery] Platform? platform, [FromQuery] string? title)
    {
        IQueryable<Game> query = db.Games;

        if (status is not null)
            query = query.Where(g => g.Status == status);

        if (platform is not null) 
            query = query.Where(g => g.Platforms.Contains(platform.Value));

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(g => g.Title.Contains(title));

        var games = await query.ToListAsync();
        return Ok(games);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Game updatedGame)
    {
        if (updatedGame.Id != id) return BadRequest("'id' in URL do not match 'id' in provided data");

        Game? existingGame = await db.Games.FindAsync(id);
        if (existingGame is null) return NotFound();

        existingGame.Title = updatedGame.Title;
        existingGame.Platforms = updatedGame.Platforms;
        existingGame.Genres = updatedGame.Genres;
        existingGame.Status = updatedGame.Status;
        existingGame.Rating =  updatedGame.Rating;

        await db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var game = await db.Games.FindAsync(id);
        if (game is null) return NotFound();

        db.Games.Remove(game);
        
        await db.SaveChangesAsync();

        return NoContent();
    }
}