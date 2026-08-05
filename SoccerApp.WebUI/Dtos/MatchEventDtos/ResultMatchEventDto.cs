using SoccerApp.WebUI.Dtos.EventTypeDtos;
using SoccerApp.WebUI.Dtos.PlayerDtos;
using SoccerApp.WebUI.Dtos.TeamDtos;

namespace SoccerApp.WebUI.Dtos.MatchEventDtos
{
    public class ResultMatchEventDto
    {
        public int Id { get; set; }
        public int MatchId { get; set; }

        public int TeamId { get; set; }
        public ResultTeamDto Team { get; set; }

        public int EventTypeId { get; set; }
        public ResultEventTypeDto EventType { get; set; }

        public int PlayerId { get; set; }
        public ResultPlayerDto Player { get; set; }

        public int? PlayerInId { get; set; }
        public ResultPlayerDto PlayerIn { get; set; }

        public int Minute { get; set; }
        public string Description { get; set; }
    }
}