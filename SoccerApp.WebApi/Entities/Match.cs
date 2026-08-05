using System.ComponentModel.DataAnnotations;
using SoccerApp.WebApi.Enums;

namespace SoccerApp.WebApi.Entities;


public class Match
{
    public int Id { get; set; }

    public int WeekId { get; set; }
    public Week Week { get; set; } = null!;

    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public int? StadiumId { get; set; }
    public Stadium? Stadium { get; set; }

    public int? RefereeId { get; set; }
    public Referee? Referee { get; set; }

    public DateTime MatchDateTime { get; set; }

    // İlk yarı skoru
    public int? HalfTimeHomeScore { get; set; }
    public int? HalfTimeAwayScore { get; set; }

    // Maç sonu skoru
    public int? FullTimeHomeScore { get; set; }
    public int? FullTimeAwayScore { get; set; }

    public MatchStatus Status { get; set; } = MatchStatus.NotPlayed;

    public int? CurrentMinute { get; set; }

    public int? Attendance { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public bool IsFeatured { get; set; }

    public ICollection<MatchEvent> MatchEvents { get; set; } = new List<MatchEvent>();
    public MatchStatistic? MatchStatistic { get; set; }
}