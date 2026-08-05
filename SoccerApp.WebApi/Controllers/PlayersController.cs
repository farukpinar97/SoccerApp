using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApiContext _context;

        public PlayersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PlayerList()
        {
            var values = _context.Players
                                 .Include(x => x.Team)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            var value = _context.Players.Find(id);
            if (value == null)
                return NotFound("Oyuncu bulunamadi.");

            return Ok(value);
        }


        [HttpGet("byteam/{teamId}")]
        public IActionResult GetPlayersByTeam(int teamId)
        {
            var values = _context.Players
                                 .Where(x => x.TeamId == teamId)
                                 .OrderBy(x => x.FullName)
                                 .ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return Ok("Oyuncu eklendi.");
        }

        [HttpPut]
        public IActionResult UpdatePlayer(Player player)
        {
            var value = _context.Players.Find(player.Id);
            if (value == null)
                return NotFound("Oyuncu bulunamadi.");

            value.TeamId = player.TeamId;
            value.FullName = player.FullName;
            value.ShirtNumber = player.ShirtNumber;
            value.Position = player.Position;

            _context.SaveChanges();
            return Ok("Oyuncu guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var value = _context.Players.Find(id);
            if (value == null)
                return NotFound("Oyuncu bulunamadi.");

            _context.Players.Remove(value);
            _context.SaveChanges();
            return Ok("Oyuncu silindi.");
        }
    }
}