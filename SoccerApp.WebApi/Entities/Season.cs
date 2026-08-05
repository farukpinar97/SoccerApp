using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoccerApp.WebApi.Entities;

public class Season
{
    public int Id { get; set; }

    public int LeagueId { get; set; }
    public League League { get; set; } = null!;

    [Required, MaxLength(20)]
    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsCurrent { get; set; }

    [JsonIgnore]
    public ICollection<Week> Weeks { get; set; } = new List<Week>();
}