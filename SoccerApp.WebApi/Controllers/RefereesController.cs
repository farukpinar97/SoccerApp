using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereesController : ControllerBase
    {
        private readonly ApiContext _context;

        public RefereesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult RefereeList()
        {
            var values = _context.Referees.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetReferee(int id)
        {
            var value = _context.Referees.Find(id);
            if (value == null)
                return NotFound("Hakem bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateReferee(Referee referee)
        {
            _context.Referees.Add(referee);
            _context.SaveChanges();
            return Ok("Hakem eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateReferee(Referee referee)
        {
            var value = _context.Referees.Find(referee.Id);
            if (value == null)
                return NotFound("Hakem bulunamadi.");

            value.FullName = referee.FullName;
            value.Country = referee.Country;

            _context.SaveChanges();
            return Ok("Hakem guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReferee(int id)
        {
            var value = _context.Referees.Find(id);
            if (value == null)
                return NotFound("Hakem bulunamadi.");

            _context.Referees.Remove(value);
            _context.SaveChanges();
            return Ok("Hakem silindi.");
        }
    }
}