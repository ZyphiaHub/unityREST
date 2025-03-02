﻿using Microsoft.AspNetCore.Mvc;
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

    // 🔹 Játékos adatainak frissítése (PUT /api/players/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(int id, PlayerData updatedPlayer)
    {
        if (id != updatedPlayer.Id)
        {
            return BadRequest("ID mismatch");
        }

        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        // Frissítsd a játékos adatait
        player.Name = updatedPlayer.Name;
        player.Score = updatedPlayer.Score;
        player.IslandId = updatedPlayer.IslandId;
        player.CharacterId = updatedPlayer.CharacterId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Players.Any(p => p.Id == id))
            {
                return NotFound();
            } else
            {
                throw;
            }
        }

        return NoContent();
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
