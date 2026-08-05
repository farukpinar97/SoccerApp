using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SoccerApp.WebApi.Entities;

public class Stadium
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = null!;

    [MaxLength(80)]
    public string? City { get; set; }

    public int? Capacity { get; set; }

    [JsonIgnore]
    public ICollection<Team> Teams { get; set; } = new List<Team>();

    [JsonIgnore]
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}