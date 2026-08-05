using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Dtos.StandingDtos;
using SoccerApp.WebApi.Enums;

namespace SoccerApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StandingsController(ApiContext context)
        {
            _context = context;
        }


        /// Galibiyet 3, beraberlik 1, maglubiyet 0 puan.
        [HttpGet]
        public IActionResult StandingList()
        {
            // Sadece tamamlanmis ve skoru girilmis maclar hesaba katilir
            var matches = _context.Matches
                .Where(x => x.Status == MatchStatus.Completed
                         && x.FullTimeHomeScore != null
                         && x.FullTimeAwayScore != null)
                .OrderBy(x => x.MatchDateTime)
                .ToList();

            var teams = _context.Teams.ToList();
            var table = new List<StandingDto>();

            foreach (var team in teams)
            {
                var row = new StandingDto
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    ShortName = team.ShortName,
                    LogoUrl = team.LogoUrl
                };

                var teamMatches = matches
                    .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
                    .ToList();

                foreach (var m in teamMatches)
                {
                    bool isHome = m.HomeTeamId == team.Id;

                    int scored = isHome ? m.FullTimeHomeScore!.Value : m.FullTimeAwayScore!.Value;
                    int conceded = isHome ? m.FullTimeAwayScore!.Value : m.FullTimeHomeScore!.Value;

                    row.Played++;
                    row.GoalsFor += scored;
                    row.GoalsAgainst += conceded;

                    if (scored > conceded)
                    {
                        row.Won++;
                        row.Points += 3;
                    }
                    else if (scored == conceded)
                    {
                        row.Drawn++;
                        row.Points += 1;
                    }
                    else
                    {
                        row.Lost++;
                    }
                }

                row.GoalDifference = row.GoalsFor - row.GoalsAgainst;

                // Son 5 mac formu — once tarihe gore tersten al, sonra eskiden yeniye cevir
                row.Form = teamMatches
                    .OrderByDescending(m => m.MatchDateTime)
                    .Take(5)
                    .Select(m =>
                    {
                        bool isHome = m.HomeTeamId == team.Id;
                        int scored = isHome ? m.FullTimeHomeScore!.Value : m.FullTimeAwayScore!.Value;
                        int conceded = isHome ? m.FullTimeAwayScore!.Value : m.FullTimeHomeScore!.Value;

                        if (scored > conceded) return "W";
                        if (scored == conceded) return "D";
                        return "L";
                    })
                    .Reverse()
                    .ToList();

                table.Add(row);
            }

            // Siralama: puan > averaj > atilan gol > isim
            var ordered = table
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
                .ThenByDescending(x => x.GoalsFor)
                .ThenBy(x => x.TeamName)
                .ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                ordered[i].Position = i + 1;
                ordered[i].Zone = GetZone(i + 1, ordered.Count);
            }

            return Ok(ordered);
        }

        [HttpGet("byteam/{teamId}")]
        public IActionResult GetStandingByTeam(int teamId)
        {
            var result = StandingList() as OkObjectResult;
            var list = result!.Value as List<StandingDto>;

            var value = list!.FirstOrDefault(x => x.TeamId == teamId);
            if (value == null)
                return NotFound("Takim bulunamadi.");

            return Ok(value);
        }

        // Temadaki satir renkleri icin bolge kodu
        private static string GetZone(int position, int totalTeams)
        {
            if (position == 1) return "champ";              // Sampiyon
            if (position <= 4) return "ucl";                // Sampiyonlar Ligi
            if (position <= 6) return "uel";                // Avrupa Ligi
            if (position > totalTeams - 3) return "rel";    // Kume dusme
            return "none";
        }
    }
}