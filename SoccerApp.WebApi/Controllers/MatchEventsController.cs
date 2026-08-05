using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Dtos.MatchEventDtos;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchEventsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MatchEventList()
        {
            var values = _context.MatchEvents
                                 .Include(x => x.Team)
                                 .Include(x => x.Player)
                                 .Include(x => x.PlayerIn)
                                 .Include(x => x.EventType)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetMatchEvent(int id)
        {
            var value = _context.MatchEvents.Find(id);
            if (value == null)
                return NotFound("Mac olayi bulunamadi.");

            return Ok(value);
        }


        [HttpGet("bymatch/{matchId}")]
        public IActionResult GetEventsByMatch(int matchId)
        {
            var values = _context.MatchEvents
                                 .Include(x => x.Team)
                                 .Include(x => x.Player)
                                 .Include(x => x.PlayerIn)
                                 .Include(x => x.EventType)
                                 .Where(x => x.MatchId == matchId)
                                 .OrderBy(x => x.Minute)
                                 .ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMatchEvent(CreateMatchEventDto dto)
        {
            var matchEvent = new MatchEvent
            {
                MatchId = dto.MatchId,
                TeamId = dto.TeamId,
                EventTypeId = dto.EventTypeId,
                PlayerId = dto.PlayerId,
                PlayerInId = dto.PlayerInId,
                Minute = dto.Minute,
                Description = dto.Description
            };

            _context.MatchEvents.Add(matchEvent);
            _context.SaveChanges();
            return Ok("Mac olayi eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateMatchEvent(MatchEvent matchEvent)
        {
            var value = _context.MatchEvents.Find(matchEvent.Id);
            if (value == null)
                return NotFound("Mac olayi bulunamadi.");

            value.MatchId = matchEvent.MatchId;
            value.TeamId = matchEvent.TeamId;
            value.EventTypeId = matchEvent.EventTypeId;
            value.PlayerId = matchEvent.PlayerId;
            value.PlayerInId = matchEvent.PlayerInId;
            value.Minute = matchEvent.Minute;
            value.Description = matchEvent.Description;

            _context.SaveChanges();
            return Ok("Mac olayi guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatchEvent(int id)
        {
            var value = _context.MatchEvents.Find(id);
            if (value == null)
                return NotFound("Mac olayi bulunamadi.");

            _context.MatchEvents.Remove(value);
            _context.SaveChanges();
            return Ok("Mac olayi silindi.");
        }
    }
}