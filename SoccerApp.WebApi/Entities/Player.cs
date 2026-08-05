using System.ComponentModel.DataAnnotations;

namespace SoccerApp.WebApi.Entities;

public class Player
{
    public int Id { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    [Required, MaxLength(100)]
    public string FullName { get; set; } = null!;

    public int? ShirtNumber { get; set; }

    [MaxLength(30)]
    public string? Position { get; set; }
}