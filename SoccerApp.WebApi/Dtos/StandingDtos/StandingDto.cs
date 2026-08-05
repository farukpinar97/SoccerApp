namespace SoccerApp.WebApi.Dtos.StandingDtos
{

    public class StandingDto
    {
        public int Position { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string? LogoUrl { get; set; }

        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }

        public List<string> Form { get; set; } = new List<string>();

        public string Zone { get; set; } = "none";
    }
}