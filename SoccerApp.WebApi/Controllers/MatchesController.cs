using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;
using SoccerApp.WebApi.Enums;
using SoccerApp.WebApi.Dtos.MatchDtos;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MatchList()
        {
            var values = _context.Matches
                                 .Include(x => x.HomeTeam)
                                 .Include(x => x.AwayTeam)
                                 .Include(x => x.Stadium)
                                 .Include(x => x.Week)
                                 .OrderBy(x => x.MatchDateTime)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetMatch(int id)
        {
            var value = _context.Matches
                                .Include(x => x.HomeTeam)
                                .Include(x => x.AwayTeam)
                                .Include(x => x.Stadium)
                                .Include(x => x.Referee)
                                .Include(x => x.Week)
                                .FirstOrDefault(x => x.Id == id);

            if (value == null)
                return NotFound("Mac bulunamadi.");

            return Ok(value);
        }

        [HttpGet("byweek/{weekNumber}")]
        public IActionResult GetMatchesByWeek(int weekNumber)
        {
            var values = _context.Matches
                                 .Include(x => x.HomeTeam)
                                 .Include(x => x.AwayTeam)
                                 .Include(x => x.Stadium)
                                 .Where(x => x.Week.WeekNumber == weekNumber)
                                 .OrderBy(x => x.MatchDateTime)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("bystatus/{status}")]
        public IActionResult GetMatchesByStatus(MatchStatus status)
        {
            var values = _context.Matches
                                 .Include(x => x.HomeTeam)
                                 .Include(x => x.AwayTeam)
                                 .Where(x => x.Status == status)
                                 .OrderBy(x => x.MatchDateTime)
                                 .ToList();
            return Ok(values);
        }


        [HttpGet("nextweek")]
        public IActionResult GetNextWeekMatches()
        {
            
            var nextWeekId = _context.Matches
                                     .Where(x => x.Status == MatchStatus.NotPlayed)
                                     .OrderBy(x => x.MatchDateTime)
                                     .Select(x => x.WeekId)
                                     .FirstOrDefault();

            if (nextWeekId == 0)
                return NotFound("Oynanacak mac bulunamadi.");

            var values = _context.Matches
                                 .Include(x => x.HomeTeam)
                                 .Include(x => x.AwayTeam)
                                 .Include(x => x.Stadium)
                                 .Include(x => x.Week)
                                 .Where(x => x.WeekId == nextWeekId)
                                 .OrderBy(x => x.MatchDateTime)
                                 .ToList();

            return Ok(values);
        }


        [HttpGet("lastmatches/{teamId}")]
        public IActionResult GetLastMatchesByTeam(int teamId, int count = 5)
        {
            var values = _context.Matches
                                 .Include(x => x.HomeTeam)
                                 .Include(x => x.AwayTeam)
                                 .Where(x => x.Status == MatchStatus.Completed &&
                                            (x.HomeTeamId == teamId || x.AwayTeamId == teamId))
                                 .OrderByDescending(x => x.MatchDateTime)
                                 .Take(count)
                                 .ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMatch(CreateMatchDto dto)
        {
            if (dto.HomeTeamId == dto.AwayTeamId)
                return BadRequest("Ev sahibi ve deplasman takimi ayni olamaz.");

            var match = new Match
            {
                WeekId = dto.WeekId,
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                StadiumId = dto.StadiumId,
                RefereeId = dto.RefereeId,
                MatchDateTime = dto.MatchDateTime,
                HalfTimeHomeScore = dto.HalfTimeHomeScore,
                HalfTimeAwayScore = dto.HalfTimeAwayScore,
                FullTimeHomeScore = dto.FullTimeHomeScore,
                FullTimeAwayScore = dto.FullTimeAwayScore,
                Status = dto.Status,
                CurrentMinute = dto.CurrentMinute,
                Attendance = dto.Attendance,
                ImageUrl = dto.ImageUrl,
                IsFeatured = dto.IsFeatured
            };

            _context.Matches.Add(match);
            _context.SaveChanges();
            return Ok("Mac eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateMatch(Match match)
        {
            var value = _context.Matches.Find(match.Id);
            if (value == null)
                return NotFound("Mac bulunamadi.");

            value.WeekId = match.WeekId;
            value.HomeTeamId = match.HomeTeamId;
            value.AwayTeamId = match.AwayTeamId;
            value.StadiumId = match.StadiumId;
            value.RefereeId = match.RefereeId;
            value.MatchDateTime = match.MatchDateTime;
            value.HalfTimeHomeScore = match.HalfTimeHomeScore;
            value.HalfTimeAwayScore = match.HalfTimeAwayScore;
            value.FullTimeHomeScore = match.FullTimeHomeScore;
            value.FullTimeAwayScore = match.FullTimeAwayScore;
            value.Status = match.Status;
            value.CurrentMinute = match.CurrentMinute;
            value.Attendance = match.Attendance;
            value.ImageUrl = match.ImageUrl;
            value.IsFeatured = match.IsFeatured;

            _context.SaveChanges();
            return Ok("Mac guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            var value = _context.Matches.Find(id);
            if (value == null)
                return NotFound("Mac bulunamadi.");

            _context.Matches.Remove(value);
            _context.SaveChanges();
            return Ok("Mac silindi.");
        }
    }
}