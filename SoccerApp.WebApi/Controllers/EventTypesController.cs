using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypesController : ControllerBase
    {
        private readonly ApiContext _context;

        public EventTypesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EventTypeList()
        {
            var values = _context.EventTypes.ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventType(int id)
        {
            var value = _context.EventTypes.Find(id);
            if (value == null)
                return NotFound("Olay turu bulunamadi.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateEventType(EventType eventType)
        {
            _context.EventTypes.Add(eventType);
            _context.SaveChanges();
            return Ok("Olay turu eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateEventType(EventType eventType)
        {
            var value = _context.EventTypes.Find(eventType.Id);
            if (value == null)
                return NotFound("Olay turu bulunamadi.");

            value.Name = eventType.Name;
            value.Code = eventType.Code;

            _context.SaveChanges();
            return Ok("Olay turu guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEventType(int id)
        {
            var value = _context.EventTypes.Find(id);
            if (value == null)
                return NotFound("Olay turu bulunamadi.");

            _context.EventTypes.Remove(value);
            _context.SaveChanges();
            return Ok("Olay turu silindi.");
        }
    }
}