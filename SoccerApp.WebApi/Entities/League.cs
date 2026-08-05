using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoccerApp.WebApi.Entities;

public class League
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(60)]
    public string? Country { get; set; }

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    [JsonIgnore]
    public ICollection<Season> Seasons { get; set; } = new List<Season>();
}