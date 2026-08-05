using SoccerApp.WebUI.Dtos.RefereeDtos;
using SoccerApp.WebUI.Dtos.StadiumDtos;
using SoccerApp.WebUI.Dtos.TeamDtos;
using SoccerApp.WebUI.Dtos.WeekDtos;
using SoccerApp.WebUI.Enums;

namespace SoccerApp.WebUI.Dtos.MatchDtos
{
    public class ResultMatchDto
    {
        public int Id { get; set; }

        public int WeekId { get; set; }
        public ResultWeekDto Week { get; set; }

        public int HomeTeamId { get; set; }
        public ResultTeamDto HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public ResultTeamDto AwayTeam { get; set; }

        public int? StadiumId { get; set; }
        public ResultStadiumDto Stadium { get; set; }

        public int? RefereeId { get; set; }
        public ResultRefereeDto Referee { get; set; }

        public DateTime MatchDateTime { get; set; }

        public int? HalfTimeHomeScore { get; set; }
        public int? HalfTimeAwayScore { get; set; }
        public int? FullTimeHomeScore { get; set; }
        public int? FullTimeAwayScore { get; set; }

        public MatchStatus Status { get; set; }
        public int? CurrentMinute { get; set; }
        public int? Attendance { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }

       

        
        public string ScoreText =>
            FullTimeHomeScore.HasValue && FullTimeAwayScore.HasValue
                ? $"{FullTimeHomeScore} – {FullTimeAwayScore}"
                : "—";

        
        public string HalfTimeText =>
            HalfTimeHomeScore.HasValue && HalfTimeAwayScore.HasValue
                ? $"{HalfTimeHomeScore} – {HalfTimeAwayScore}"
                : "-";

       
        public string StatusText => Status switch
        {
            MatchStatus.Completed => "MS",
            MatchStatus.InProgress => CurrentMinute.HasValue ? $"{CurrentMinute}'" : "CANLI",
            _ => MatchDateTime.ToString("HH:mm")
        };
    }
}