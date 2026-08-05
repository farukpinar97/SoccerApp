using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly ApiContext _context;

        public SeasonsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SeasonList()
        {
            var values = _context.Seasons.Include(x => x.League).ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetSeason(int id)
        {
            var value = _context.Seasons.Find(id);
            if (value == null)
                return NotFound("Sezon bulunamadi.");

            return Ok(value);
        }

        /// Puan durumu hesaplamasinda kullanilir
        [HttpGet("current")]
        public IActionResult GetCurrentSeason()
        {
            var value = _context.Seasons.FirstOrDefault(x => x.IsCurrent);
            if (value == null)
                return NotFound("Aktif sezon bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateSeason(Season season)
        {
            _context.Seasons.Add(season);
            _context.SaveChanges();
            return Ok("Sezon eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateSeason(Season season)
        {
            var value = _context.Seasons.Find(season.Id);
            if (value == null)
                return NotFound("Sezon bulunamadi.");

            value.LeagueId = season.LeagueId;
            value.Name = season.Name;
            value.StartDate = season.StartDate;
            value.EndDate = season.EndDate;
            value.IsCurrent = season.IsCurrent;

            _context.SaveChanges();
            return Ok("Sezon guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSeason(int id)
        {
            var value = _context.Seasons.Find(id);
            if (value == null)
                return NotFound("Sezon bulunamadi.");

            _context.Seasons.Remove(value);
            _context.SaveChanges();
            return Ok("Sezon silindi.");
        }
    }
}