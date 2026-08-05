using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ApiContext _context;

        public LeaguesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LeagueList()
        {
            var values = _context.Leagues.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeague(int id)
        {
            var value = _context.Leagues.Find(id);
            if (value == null)
                return NotFound("Lig bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateLeague(League league)
        {
            _context.Leagues.Add(league);
            _context.SaveChanges();
            return Ok("Lig eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateLeague(League league)
        {
            var value = _context.Leagues.Find(league.Id);
            if (value == null)
                return NotFound("Lig bulunamadi.");

            value.Name = league.Name;
            value.Country = league.Country;
            value.LogoUrl = league.LogoUrl;

            _context.SaveChanges();
            return Ok("Lig guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeague(int id)
        {
            var value = _context.Leagues.Find(id);
            if (value == null)
                return NotFound("Lig bulunamadi.");

            _context.Leagues.Remove(value);
            _context.SaveChanges();
            return Ok("Lig silindi.");
        }
    }
}