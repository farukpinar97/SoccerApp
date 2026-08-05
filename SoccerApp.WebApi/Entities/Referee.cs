using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SoccerApp.WebApi.Entities;

public class Referee
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = null!;

    [MaxLength(60)]
    public string? Country { get; set; }

    [JsonIgnore]
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}