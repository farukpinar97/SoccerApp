using System.ComponentModel.DataAnnotations;

namespace SoccerApp.WebApi.Entities;


public class MatchEvent
{
    public int Id { get; set; }

    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;


    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public int EventTypeId { get; set; }
    public EventType EventType { get; set; } = null!;


    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;


    public int? PlayerInId { get; set; }
    public Player? PlayerIn { get; set; }

    [Range(0, 130)]
    public int Minute { get; set; }

    [MaxLength(120)]
    public string? Description { get; set; }
}