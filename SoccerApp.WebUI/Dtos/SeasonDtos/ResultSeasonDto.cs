namespace SoccerApp.WebUI.Dtos.SeasonDtos
{
    public class ResultSeasonDto
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}