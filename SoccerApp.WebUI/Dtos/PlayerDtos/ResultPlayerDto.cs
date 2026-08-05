namespace SoccerApp.WebUI.Dtos.PlayerDtos
{
    public class ResultPlayerDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string FullName { get; set; }
        public int? ShirtNumber { get; set; }
        public string Position { get; set; }
    }
}