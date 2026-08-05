namespace SoccerApp.WebUI.Dtos.WeekDtos
{
    public class ResultWeekDto
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}