using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StadiumsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult StadiumList()
        {
            var values = _context.Stadiums.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetStadium(int id)
        {
            var value = _context.Stadiums.Find(id);
            if (value == null)
                return NotFound("Stadyum bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateStadium(Stadium stadium)
        {
            _context.Stadiums.Add(stadium);
            _context.SaveChanges();
            return Ok("Stadyum eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateStadium(Stadium stadium)
        {
            var value = _context.Stadiums.Find(stadium.Id);
            if (value == null)
                return NotFound("Stadyum bulunamadi.");

            value.Name = stadium.Name;
            value.City = stadium.City;
            value.Capacity = stadium.Capacity;

            _context.SaveChanges();
            return Ok("Stadyum guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStadium(int id)
        {
            var value = _context.Stadiums.Find(id);
            if (value == null)
                return NotFound("Stadyum bulunamadi.");

            _context.Stadiums.Remove(value);
            _context.SaveChanges();
            return Ok("Stadyum silindi.");
        }
    }
}