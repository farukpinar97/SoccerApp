using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoccerApp.WebApi.Entities;

public class EventType
{
    public int Id { get; set; }

    [Required, MaxLength(40)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(20)]
    public string Code { get; set; } = null!;

    [JsonIgnore]
    public ICollection<MatchEvent> MatchEvents { get; set; } = new List<MatchEvent>();
}