namespace SoccerApp.WebUI.Dtos.MatchEventDtos
{
   
    public class CreateMatchEventDto
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int EventTypeId { get; set; }
        public int PlayerId { get; set; }

        
        public int? PlayerInId { get; set; }

        public int Minute { get; set; }
        public string Description { get; set; }
    }
}