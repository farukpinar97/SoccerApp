using SoccerApp.WebUI.Dtos.StadiumDtos;

namespace SoccerApp.WebUI.Dtos.TeamDtos
{
    public class ResultTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }
        public int? FoundedYear { get; set; }
        public int? StadiumId { get; set; }
        public ResultStadiumDto Stadium { get; set; }
    }
}