using SoccerApp.WebApi.Enums;

namespace SoccerApp.WebApi.Dtos.MatchDtos
{

    public class CreateMatchDto
    {
        public int WeekId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? StadiumId { get; set; }
        public int? RefereeId { get; set; }

        public DateTime MatchDateTime { get; set; }

        public int? HalfTimeHomeScore { get; set; }
        public int? HalfTimeAwayScore { get; set; }
        public int? FullTimeHomeScore { get; set; }
        public int? FullTimeAwayScore { get; set; }

        public MatchStatus Status { get; set; }
        public int? CurrentMinute { get; set; }
        public int? Attendance { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}