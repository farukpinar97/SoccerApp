using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Dtos.MatchStatisticDtos;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchStatisticsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchStatisticsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MatchStatisticList()
        {
            var values = _context.MatchStatistics
                                 .Include(x => x.Match)
                                 .ToList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetMatchStatistic(int id)
        {
            var value = _context.MatchStatistics.Find(id);
            if (value == null)
                return NotFound("Mac istatistigi bulunamadi.");

            return Ok(value);
        }

        [HttpGet("bymatch/{matchId}")]
        public IActionResult GetStatisticByMatch(int matchId)
        {
            var value = _context.MatchStatistics.FirstOrDefault(x => x.MatchId == matchId);
            if (value == null)
                return NotFound("Bu mac icin istatistik girilmemis.");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateMatchStatistic(CreateMatchStatisticDto dto)
        {
            // Her mac icin tek istatistik kaydi
            var exists = _context.MatchStatistics.Any(x => x.MatchId == dto.MatchId);
            if (exists)
                return BadRequest("Bu mac icin istatistik zaten girilmis.");

            var statistic = new MatchStatistic
            {
                MatchId = dto.MatchId,
                HomePossession = dto.HomePossession,
                AwayPossession = dto.AwayPossession,
                HomeShots = dto.HomeShots,
                AwayShots = dto.AwayShots,
                HomeShotsOnTarget = dto.HomeShotsOnTarget,
                AwayShotsOnTarget = dto.AwayShotsOnTarget,
                HomePasses = dto.HomePasses,
                AwayPasses = dto.AwayPasses,
                HomePassAccuracy = dto.HomePassAccuracy,
                AwayPassAccuracy = dto.AwayPassAccuracy,
                HomeCorners = dto.HomeCorners,
                AwayCorners = dto.AwayCorners,
                HomeFouls = dto.HomeFouls,
                AwayFouls = dto.AwayFouls,
                HomeOffsides = dto.HomeOffsides,
                AwayOffsides = dto.AwayOffsides,
                HomeYellowCards = dto.HomeYellowCards,
                AwayYellowCards = dto.AwayYellowCards,
                HomeRedCards = dto.HomeRedCards,
                AwayRedCards = dto.AwayRedCards
            };

            _context.MatchStatistics.Add(statistic);
            _context.SaveChanges();
            return Ok("Mac istatistigi eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateMatchStatistic(MatchStatistic matchStatistic)
        {
            var value = _context.MatchStatistics.Find(matchStatistic.Id);
            if (value == null)
                return NotFound("Mac istatistigi bulunamadi.");

            value.MatchId = matchStatistic.MatchId;
            value.HomePossession = matchStatistic.HomePossession;
            value.AwayPossession = matchStatistic.AwayPossession;
            value.HomeShots = matchStatistic.HomeShots;
            value.AwayShots = matchStatistic.AwayShots;
            value.HomeShotsOnTarget = matchStatistic.HomeShotsOnTarget;
            value.AwayShotsOnTarget = matchStatistic.AwayShotsOnTarget;
            value.HomePasses = matchStatistic.HomePasses;
            value.AwayPasses = matchStatistic.AwayPasses;
            value.HomePassAccuracy = matchStatistic.HomePassAccuracy;
            value.AwayPassAccuracy = matchStatistic.AwayPassAccuracy;
            value.HomeCorners = matchStatistic.HomeCorners;
            value.AwayCorners = matchStatistic.AwayCorners;
            value.HomeFouls = matchStatistic.HomeFouls;
            value.AwayFouls = matchStatistic.AwayFouls;
            value.HomeOffsides = matchStatistic.HomeOffsides;
            value.AwayOffsides = matchStatistic.AwayOffsides;
            value.HomeYellowCards = matchStatistic.HomeYellowCards;
            value.AwayYellowCards = matchStatistic.AwayYellowCards;
            value.HomeRedCards = matchStatistic.HomeRedCards;
            value.AwayRedCards = matchStatistic.AwayRedCards;

            _context.SaveChanges();
            return Ok("Mac istatistigi guncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatchStatistic(int id)
        {
            var value = _context.MatchStatistics.Find(id);
            if (value == null)
                return NotFound("Mac istatistigi bulunamadi.");

            _context.MatchStatistics.Remove(value);
            _context.SaveChanges();
            return Ok("Mac istatistigi silindi.");
        }
    }
}