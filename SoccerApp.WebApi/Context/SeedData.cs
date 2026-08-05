using SoccerApp.WebApi.Context;
using SoccerApp.WebApi.Entities;
using SoccerApp.WebApi.Enums;

namespace SoccerApp.WebApi.Context
{
    public static class SeedData
    {
        // Skorlarin her seferinde ayni cikmasi icin
        private static readonly Random _rnd = new Random(42);

        public static void Initialize(ApiContext context)
        {
            var league = SeedLeague(context);
            var season = SeedSeason(context, league);
            SeedWeeks(context, season);
            SeedTeamsAndStadiums(context);
            SeedPlayers(context);
            SeedEventTypes(context);
            SeedReferees(context);
            SeedMatches(context);
            SeedMatchDetails(context);
        }

        // ─────────────────────────── LIG ───────────────────────────

        private static League SeedLeague(ApiContext context)
        {
            var league = context.Leagues.FirstOrDefault();
            if (league != null) return league;

            league = new League
            {
                Name = "Premier League",
                Country = "Ingiltere",
                LogoUrl = "https://upload.wikimedia.org/wikipedia/en/f/f2/Premier_League_Logo.svg"
            };

            context.Leagues.Add(league);
            context.SaveChanges();
            return league;
        }

        // ─────────────────────────── SEZON ───────────────────────────

        private static Season SeedSeason(ApiContext context, League league)
        {
            var season = context.Seasons.FirstOrDefault();
            if (season != null) return season;

            season = new Season
            {
                LeagueId = league.Id,
                Name = "2024/2025",
                StartDate = new DateTime(2024, 8, 16),
                EndDate = new DateTime(2025, 5, 25),
                IsCurrent = true
            };

            context.Seasons.Add(season);
            context.SaveChanges();
            return season;
        }

        // ─────────────────────────── HAFTALAR ───────────────────────────

        private static void SeedWeeks(ApiContext context, Season season)
        {
            if (context.Weeks.Any()) return;

            // 1. hafta 16 Agustos 2024 Cuma gunu basliyor, her hafta 7 gun ekleniyor
            var firstFriday = new DateTime(2024, 8, 16);
            var weeks = new List<Week>();

            for (int i = 1; i <= 38; i++)
            {
                var start = firstFriday.AddDays((i - 1) * 7);
                weeks.Add(new Week
                {
                    SeasonId = season.Id,
                    WeekNumber = i,
                    StartDate = start,
                    EndDate = start.AddDays(2)   // Cuma - Pazar
                });
            }

            context.Weeks.AddRange(weeks);
            context.SaveChanges();
        }

        // ─────────────────────── TAKIMLAR + STADYUMLAR ───────────────────────

        private static void SeedTeamsAndStadiums(ApiContext context)
        {
            if (context.Teams.Any()) return;

            var data = new (string Name, string Short, string Logo, int Founded,
                            string Stadium, string City, int Capacity)[]
            {
                ("Liverpool", "LIV", "https://upload.wikimedia.org/wikipedia/en/0/0c/Liverpool_FC.svg", 1892,
                 "Anfield", "Liverpool", 61276),
                ("Arsenal", "ARS", "https://upload.wikimedia.org/wikipedia/en/5/53/Arsenal_FC.svg", 1886,
                 "Emirates Stadium", "Londra", 60704),
                ("Manchester City", "MCI", "https://upload.wikimedia.org/wikipedia/en/e/eb/Manchester_City_FC_badge.svg", 1880,
                 "Etihad Stadium", "Manchester", 53400),
                ("Chelsea", "CHE", "https://upload.wikimedia.org/wikipedia/en/c/cc/Chelsea_FC.svg", 1905,
                 "Stamford Bridge", "Londra", 40343),
                ("Newcastle", "NEW", "https://upload.wikimedia.org/wikipedia/en/5/56/Newcastle_United_Logo.svg", 1892,
                 "St James' Park", "Newcastle", 52305),
                ("Aston Villa", "AVL", "https://upload.wikimedia.org/wikipedia/en/f/f9/Aston_Villa_FC_crest_%282016%29.svg", 1874,
                 "Villa Park", "Birmingham", 42682),
                ("Tottenham", "TOT", "https://upload.wikimedia.org/wikipedia/en/b/b4/Tottenham_Hotspur.svg", 1882,
                 "Tottenham Hotspur Stadium", "Londra", 62850),
                ("Manchester Utd", "MNU", "https://upload.wikimedia.org/wikipedia/en/7/7a/Manchester_United_FC_crest.svg", 1878,
                 "Old Trafford", "Manchester", 74310),
                ("West Ham", "WHU", "https://upload.wikimedia.org/wikipedia/en/c/c2/West_Ham_United_FC_logo.svg", 1895,
                 "London Stadium", "Londra", 62500),
                ("Brighton", "BHA", "https://upload.wikimedia.org/wikipedia/en/f/fd/Brighton_%26_Hove_Albion_FC_logo.svg", 1901,
                 "Amex Stadium", "Brighton", 31800),
                ("Brentford", "BRE", "https://upload.wikimedia.org/wikipedia/en/2/2a/Brentford_FC_crest.svg", 1889,
                 "Gtech Community Stadium", "Londra", 17250),
                ("Fulham", "FUL", "https://upload.wikimedia.org/wikipedia/en/e/eb/Fulham_FC_%28shield%29.svg", 1879,
                 "Craven Cottage", "Londra", 29589),
                ("Wolves", "WOL", "https://upload.wikimedia.org/wikipedia/en/f/fc/Wolverhampton_Wanderers.svg", 1877,
                 "Molineux Stadium", "Wolverhampton", 31750),
                ("Everton", "EVE", "https://upload.wikimedia.org/wikipedia/en/7/7c/Everton_FC_logo.svg", 1878,
                 "Goodison Park", "Liverpool", 39414),
                ("Crystal Palace", "CRY", "https://upload.wikimedia.org/wikipedia/en/a/a2/Crystal_Palace_FC_logo_%282022%29.svg", 1905,
                 "Selhurst Park", "Londra", 25486),
                ("Nottm Forest", "NFO", "https://upload.wikimedia.org/wikipedia/en/e/e5/Nottingham_Forest_FC_logo.svg", 1865,
                 "City Ground", "Nottingham", 30445),
                ("Bournemouth", "BOU", "https://upload.wikimedia.org/wikipedia/en/e/e5/AFC_Bournemouth_%282013%29.svg", 1899,
                 "Vitality Stadium", "Bournemouth", 11307),
                ("Leicester City", "LEI", "https://upload.wikimedia.org/wikipedia/en/2/2d/Leicester_City_crest.svg", 1884,
                 "King Power Stadium", "Leicester", 32262),
                ("Ipswich Town", "IPS", "https://upload.wikimedia.org/wikipedia/en/4/43/Ipswich_Town.svg", 1878,
                 "Portman Road", "Ipswich", 30311),
                ("Southampton", "SOU", "https://upload.wikimedia.org/wikipedia/en/c/c9/FC_Southampton.svg", 1885,
                 "St Mary's Stadium", "Southampton", 32384),
            };

            foreach (var t in data)
            {
                var stadium = new Stadium
                {
                    Name = t.Stadium,
                    City = t.City,
                    Capacity = t.Capacity
                };

                context.Teams.Add(new Team
                {
                    Name = t.Name,
                    ShortName = t.Short,
                    LogoUrl = t.Logo,
                    FoundedYear = t.Founded,
                    Stadium = stadium
                });
            }

            context.SaveChanges();
        }

        // ─────────────────────────── OYUNCULAR ───────────────────────────

        private static void SeedPlayers(ApiContext context)
        {
            if (context.Players.Any()) return;

            // "Gol detaylari anonim isimlerle girilebilir"
            var positions = new[]
            {
                "Kaleci", "Defans", "Defans", "Orta Saha",
                "Orta Saha", "Orta Saha", "Forvet", "Forvet"
            };

            var players = new List<Player>();

            foreach (var team in context.Teams.ToList())
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    players.Add(new Player
                    {
                        TeamId = team.Id,
                        FullName = $"{team.ShortName} Oyuncu {i + 1}",
                        ShirtNumber = i + 1,
                        Position = positions[i]
                    });
                }
            }

            context.Players.AddRange(players);
            context.SaveChanges();
        }

        // ─────────────────────────── OLAY TURLERI ───────────────────────────

        private static void SeedEventTypes(ApiContext context)
        {
            if (context.EventTypes.Any()) return;

            context.EventTypes.AddRange(
                new EventType { Name = "Gol", Code = "goal" },
                new EventType { Name = "Sari Kart", Code = "yellow" },
                new EventType { Name = "Kirmizi Kart", Code = "red" },
                new EventType { Name = "Oyuncu Degisikligi", Code = "sub" }
            );

            context.SaveChanges();
        }

        // ─────────────────────────── HAKEMLER ───────────────────────────

        private static void SeedReferees(ApiContext context)
        {
            if (context.Referees.Any()) return;

            context.Referees.AddRange(
                new Referee { FullName = "Michael Oliver", Country = "Ingiltere" },
                new Referee { FullName = "Anthony Taylor", Country = "Ingiltere" },
                new Referee { FullName = "Paul Tierney", Country = "Ingiltere" },
                new Referee { FullName = "Simon Hooper", Country = "Ingiltere" },
                new Referee { FullName = "Craig Pawson", Country = "Ingiltere" }
            );

            context.SaveChanges();
        }

        // ─────────────────────────── MACLAR ───────────────────────────

        private static void SeedMatches(ApiContext context)
        {
            if (context.Matches.Any()) return;

            var teams = context.Teams.OrderBy(x => x.Id).ToList();
            var weeks = context.Weeks.OrderBy(x => x.WeekNumber).Take(5).ToList();
            var refereeIds = context.Referees.Select(x => x.Id).ToList();

            // Mac saatleri: 1 Cuma, 5 Cumartesi, 4 Pazar maci
            var slots = new (int DayOffset, int Hour, int Minute)[]
            {
                (0, 20, 0),                                        // Cuma
                (1, 12, 30), (1, 15, 0), (1, 15, 0), (1, 15, 0), (1, 17, 30),  // Cumartesi
                (2, 14, 0), (2, 14, 0), (2, 16, 30), (2, 19, 0)    // Pazar
            };

            var matches = new List<Match>();

            for (int w = 0; w < weeks.Count; w++)
            {
                var week = weeks[w];
                var pairs = GetRoundPairs(teams, w);   // O haftanin 10 eslesmesi

                for (int i = 0; i < pairs.Count; i++)
                {
                    var (home, away) = pairs[i];
                    var slot = slots[i];

                    var match = new Match
                    {
                        WeekId = week.Id,
                        HomeTeamId = home.Id,
                        AwayTeamId = away.Id,
                        StadiumId = home.StadiumId,
                        RefereeId = refereeIds[_rnd.Next(refereeIds.Count)],
                        MatchDateTime = week.StartDate
                                            .AddDays(slot.DayOffset)
                                            .AddHours(slot.Hour)
                                            .AddMinutes(slot.Minute)
                    };

                    // 1-3. hafta: tamami tamamlandi
                    // 4. hafta: 7 tamamlandi + 3 devam ediyor
                    // 5. hafta: tamami oynanmadi  (fixtures.html bu haftayi gosterecek)
                    if (w <= 2)
                        SetCompleted(match, home, away);
                    else if (w == 3)
                    {
                        if (i < 7) SetCompleted(match, home, away);
                        else SetInProgress(match);
                    }
                    else
                    {
                        match.Status = MatchStatus.NotPlayed;
                    }

                    matches.Add(match);
                }
            }

            // 4. haftanin ilk maci "One Cikan Mac" olsun
            var featured = matches.FirstOrDefault(x => x.Status == MatchStatus.Completed
                                                    && x.WeekId == weeks[3].Id);
            if (featured != null)
                featured.IsFeatured = true;

            context.Matches.AddRange(matches);
            context.SaveChanges();
        }

        private static void SetCompleted(Match match, Team home, Team away)
        {
            // Ev sahibi avantaji: 0-3 arasi, deplasman 0-2 arasi agirlikli
            int homeGoals = WeightedGoals(true);
            int awayGoals = WeightedGoals(false);

            match.FullTimeHomeScore = homeGoals;
            match.FullTimeAwayScore = awayGoals;

            // Ilk yari skoru mac sonu skorunu asamaz
            match.HalfTimeHomeScore = homeGoals == 0 ? 0 : _rnd.Next(0, homeGoals + 1);
            match.HalfTimeAwayScore = awayGoals == 0 ? 0 : _rnd.Next(0, awayGoals + 1);

            match.Status = MatchStatus.Completed;
            match.Attendance = _rnd.Next(20000, 60000);
        }

        /// <summary>Devam eden mac: anlik skor ve dakika.</summary>
        private static void SetInProgress(Match match)
        {
            int minute = _rnd.Next(20, 88);

            match.Status = MatchStatus.InProgress;
            match.CurrentMinute = minute;
            match.FullTimeHomeScore = _rnd.Next(0, 3);
            match.FullTimeAwayScore = _rnd.Next(0, 3);

            // Ilk yari gecildiyse IY skoru da olur
            if (minute > 45)
            {
                match.HalfTimeHomeScore = _rnd.Next(0, match.FullTimeHomeScore.Value + 1);
                match.HalfTimeAwayScore = _rnd.Next(0, match.FullTimeAwayScore.Value + 1);
            }

            match.Attendance = _rnd.Next(20000, 60000);
        }

       
        private static int WeightedGoals(bool isHome)
        {
            int roll = _rnd.Next(100);

            if (isHome)
                return roll < 22 ? 0 : roll < 55 ? 1 : roll < 80 ? 2 : roll < 94 ? 3 : 4;

            return roll < 30 ? 0 : roll < 62 ? 1 : roll < 86 ? 2 : roll < 97 ? 3 : 4;
        }


        private static List<(Team Home, Team Away)> GetRoundPairs(List<Team> teams, int round)
        {
            int n = teams.Count;
            var rotation = new List<Team>(teams);

            // Ilk takim sabit, digerleri round kadar dondurulur
            for (int r = 0; r < round; r++)
            {
                var last = rotation[n - 1];
                rotation.RemoveAt(n - 1);
                rotation.Insert(1, last);
            }

            var pairs = new List<(Team, Team)>();

            for (int i = 0; i < n / 2; i++)
            {
                var a = rotation[i];
                var b = rotation[n - 1 - i];

                // Haftalar arasi ev sahipligi donsun
                if ((round + i) % 2 == 0)
                    pairs.Add((a, b));
                else
                    pairs.Add((b, a));
            }

            return pairs;
        }

        // ──────────────── MAC OLAYLARI + ISTATISTIKLER ────────────────

        private static void SeedMatchDetails(ApiContext context)
        {
            if (context.MatchEvents.Any()) return;

            var goalType = context.EventTypes.First(x => x.Code == "goal");
            var yellowType = context.EventTypes.First(x => x.Code == "yellow");
            var subType = context.EventTypes.First(x => x.Code == "sub");

            var targetMatches = context.Matches
                .Where(x => x.Status == MatchStatus.Completed)
                .OrderByDescending(x => x.MatchDateTime)
                .Take(5)
                .ToList();

            var events = new List<MatchEvent>();
            var statistics = new List<MatchStatistic>();

            foreach (var match in targetMatches)
            {
                var homePlayers = context.Players.Where(p => p.TeamId == match.HomeTeamId).ToList();
                var awayPlayers = context.Players.Where(p => p.TeamId == match.AwayTeamId).ToList();

                // ── Goller ── (skorla tutarli)
                var minutes = new List<int>();

                for (int i = 0; i < match.FullTimeHomeScore!.Value; i++)
                {
                    int minute = NextUniqueMinute(minutes);
                    events.Add(new MatchEvent
                    {
                        MatchId = match.Id,
                        TeamId = match.HomeTeamId,
                        EventTypeId = goalType.Id,
                        PlayerId = PickAttacker(homePlayers).Id,
                        Minute = minute,
                        Description = RandomGoalType()
                    });
                }

                for (int i = 0; i < match.FullTimeAwayScore!.Value; i++)
                {
                    int minute = NextUniqueMinute(minutes);
                    events.Add(new MatchEvent
                    {
                        MatchId = match.Id,
                        TeamId = match.AwayTeamId,
                        EventTypeId = goalType.Id,
                        PlayerId = PickAttacker(awayPlayers).Id,
                        Minute = minute,
                        Description = RandomGoalType()
                    });
                }

                // ── Sari kartlar (her takima 1) ──
                int homeYellow = 1, awayYellow = 1;

                events.Add(new MatchEvent
                {
                    MatchId = match.Id,
                    TeamId = match.HomeTeamId,
                    EventTypeId = yellowType.Id,
                    PlayerId = homePlayers[_rnd.Next(homePlayers.Count)].Id,
                    Minute = NextUniqueMinute(minutes),
                    Description = "Faul"
                });

                events.Add(new MatchEvent
                {
                    MatchId = match.Id,
                    TeamId = match.AwayTeamId,
                    EventTypeId = yellowType.Id,
                    PlayerId = awayPlayers[_rnd.Next(awayPlayers.Count)].Id,
                    Minute = NextUniqueMinute(minutes),
                    Description = "Mudahale"
                });

                // ── Oyuncu degisiklikleri (her takima 1) ──
                events.Add(new MatchEvent
                {
                    MatchId = match.Id,
                    TeamId = match.HomeTeamId,
                    EventTypeId = subType.Id,
                    PlayerId = homePlayers[2].Id,      // cikan
                    PlayerInId = homePlayers[6].Id,    // giren
                    Minute = _rnd.Next(55, 80),
                    Description = "Taktik degisiklik"
                });

                events.Add(new MatchEvent
                {
                    MatchId = match.Id,
                    TeamId = match.AwayTeamId,
                    EventTypeId = subType.Id,
                    PlayerId = awayPlayers[3].Id,
                    PlayerInId = awayPlayers[7].Id,
                    Minute = _rnd.Next(55, 85),
                    Description = "Taktik degisiklik"
                });

                // ── Istatistikler ── (skorla uyumlu uretiliyor)
                int homePossession = _rnd.Next(40, 61);
                int homeShots = 6 + match.FullTimeHomeScore.Value * 2 + _rnd.Next(0, 6);
                int awayShots = 5 + match.FullTimeAwayScore.Value * 2 + _rnd.Next(0, 6);

                statistics.Add(new MatchStatistic
                {
                    MatchId = match.Id,
                    HomePossession = homePossession,
                    AwayPossession = 100 - homePossession,
                    HomeShots = homeShots,
                    AwayShots = awayShots,
                    HomeShotsOnTarget = Math.Max(match.FullTimeHomeScore.Value, homeShots / 2),
                    AwayShotsOnTarget = Math.Max(match.FullTimeAwayScore.Value, awayShots / 2),
                    HomePasses = 300 + homePossession * 4,
                    AwayPasses = 300 + (100 - homePossession) * 4,
                    HomePassAccuracy = _rnd.Next(78, 92),
                    AwayPassAccuracy = _rnd.Next(75, 90),
                    HomeCorners = _rnd.Next(2, 10),
                    AwayCorners = _rnd.Next(1, 8),
                    HomeFouls = _rnd.Next(7, 16),
                    AwayFouls = _rnd.Next(8, 18),
                    HomeOffsides = _rnd.Next(0, 5),
                    AwayOffsides = _rnd.Next(0, 5),
                    HomeYellowCards = homeYellow,
                    AwayYellowCards = awayYellow,
                    HomeRedCards = 0,
                    AwayRedCards = 0
                });
            }

            context.MatchEvents.AddRange(events);
            context.MatchStatistics.AddRange(statistics);
            context.SaveChanges();
        }

        // Ayni dakikaya iki olay dusmesin.
        private static int NextUniqueMinute(List<int> used)
        {
            int minute;
            do
            {
                minute = _rnd.Next(3, 91);
            }
            while (used.Contains(minute));

            used.Add(minute);
            return minute;
        }

        // Golu genelde orta saha/forvet atsin.
        private static Player PickAttacker(List<Player> players)
        {
            var attackers = players
                .Where(p => p.Position == "Forvet" || p.Position == "Orta Saha")
                .ToList();

            return attackers.Count > 0
                ? attackers[_rnd.Next(attackers.Count)]
                : players[_rnd.Next(players.Count)];
        }

        private static string RandomGoalType()
        {
            var types = new[] { "Duz Sut", "Kafa Golu", "Penalti", "Sol Kose", "Sag Kose", "Kontra Atak" };
            return types[_rnd.Next(types.Length)];
        }
    }
}