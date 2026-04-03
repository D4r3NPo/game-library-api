using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    // TODO replace by database
    readonly Dictionary<string, Game> games = new() {
        {"doom", new Game("doom","Doom", [Platform.Linux, Platform.Windows], [Genre.Multiplayer, Genre.FPS], Status.ToDo, null)},
        {"factorio", new Game("factorio","Factorio", [Platform.MacOS, Platform.Linux, Platform.Windows], [Genre.Factory, Genre.Multiplayer], Status.Done, 7)},
    };

    [HttpGet]
    public IActionResult Get() => Ok(games.Values);

    [HttpGet("{id}")]
    public IActionResult GetById(string id) => games.TryGetValue(id, out var game) ? Ok(game) : NotFound();

    [HttpPost]
    public IActionResult Create([FromBody] Game game)
    {
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }
}