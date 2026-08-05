using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private readonly ApiContext _context;

        public WeeksController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult WeekList()
        {
            var values = _context.Weeks
                                 .OrderBy(x => x.WeekNumber)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetWeek(int id)
        {
            var value = _context.Weeks.Find(id);
            if (value == null)
                return NotFound("Hafta bulunamadi.");

            return Ok(value);
        }

        [HttpGet("bynumber/{weekNumber}")]
        public IActionResult GetWeekByNumber(int weekNumber)
        {
            var value = _context.Weeks.FirstOrDefault(x => x.WeekNumber == weekNumber);
            if (value == null)
                return NotFound("Hafta bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateWeek(Week week)
        {
            _context.Weeks.Add(week);
            _context.SaveChanges();
            return Ok("Hafta eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateWeek(Week week)
        {
            var value = _context.Weeks.Find(week.Id);
            if (value == null)
                return NotFound("Hafta bulunamadi.");

            value.SeasonId = week.SeasonId;
            value.WeekNumber = week.WeekNumber;
            value.StartDate = week.StartDate;
            value.EndDate = week.EndDate;

            _context.SaveChanges();
            return Ok("Hafta guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWeek(int id)
        {
            var value = _context.Weeks.Find(id);
            if (value == null)
                return NotFound("Hafta bulunamadi.");

            _context.Weeks.Remove(value);
            _context.SaveChanges();
            return Ok("Hafta silindi.");
        }
    }
}