using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SoccerApp.WebApi.Entities;

public class Team
{
    public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(3)]
    public string ShortName { get; set; } = null!;

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    public int? FoundedYear { get; set; }

    public int? StadiumId { get; set; }
    public Stadium? Stadium { get; set; }

    [JsonIgnore]
    public ICollection<Player> Players { get; set; } = new List<Player>();

    [JsonIgnore]
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

    [JsonIgnore]
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}