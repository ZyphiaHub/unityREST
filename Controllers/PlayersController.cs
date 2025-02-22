using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase {
    private readonly MyDbContext _context;

    public PlayersController(MyDbContext context)
    {
        _context = context;
    }

    // 🔹 Összes játékos lekérdezése (GET /api/players)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerData>>> GetPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    // 🔹 Egy játékos lekérdezése ID alapján (GET /api/players/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerData>> GetPlayer(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }
        return player;
    }

    // 🔹 Új játékos létrehozása (POST /api/players)
    [HttpPost]
    public async Task<ActionResult<PlayerData>> PostPlayer(PlayerData player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
    }

    // 🔹 Játékos törlése (DELETE /api/players/1)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }
        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
