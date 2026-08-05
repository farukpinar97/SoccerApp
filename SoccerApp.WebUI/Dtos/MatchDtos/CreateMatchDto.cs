using SoccerApp.WebUI.Enums;

namespace SoccerApp.WebUI.Dtos.MatchDtos
{
    
    public class CreateMatchDto
    {
        public int WeekId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? StadiumId { get; set; }
        public int? RefereeId { get; set; }
        public DateTime MatchDateTime { get; set; } =
    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                 DateTime.Now.Hour, 0, 0);

        public int? HalfTimeHomeScore { get; set; }
        public int? HalfTimeAwayScore { get; set; }
        public int? FullTimeHomeScore { get; set; }
        public int? FullTimeAwayScore { get; set; }

        public MatchStatus Status { get; set; } = MatchStatus.NotPlayed;
        public int? CurrentMinute { get; set; }
        public int? Attendance { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}