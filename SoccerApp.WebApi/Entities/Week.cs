using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SoccerApp.WebApi.Entities;


public class Week
{
    public int Id { get; set; }

    public int SeasonId { get; set; }
    public Season Season { get; set; } = null!;

    public int WeekNumber { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}