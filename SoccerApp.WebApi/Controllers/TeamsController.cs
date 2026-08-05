using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TeamList()
        {
            var values = _context.Teams
                                 .Include(x => x.Stadium)
                                 .OrderBy(x => x.Name)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(int id)
        {
            var value = _context.Teams
                                .Include(x => x.Stadium)
                                .FirstOrDefault(x => x.Id == id);
            if (value == null)
                return NotFound("Takim bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok("Takim eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateTeam(Team team)
        {
            var value = _context.Teams.Find(team.Id);
            if (value == null)
                return NotFound("Takim bulunamadi.");

            value.Name = team.Name;
            value.ShortName = team.ShortName;
            value.LogoUrl = team.LogoUrl;
            value.FoundedYear = team.FoundedYear;
            value.StadiumId = team.StadiumId;

            _context.SaveChanges();
            return Ok("Takim guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var value = _context.Teams.Find(id);
            if (value == null)
                return NotFound("Takim bulunamadi.");

            _context.Teams.Remove(value);
            _context.SaveChanges();
            return Ok("Takim silindi.");
        }
    }
}