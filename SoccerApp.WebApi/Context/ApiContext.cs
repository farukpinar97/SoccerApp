using Microsoft.EntityFrameworkCore;
using SoccerApp.WebApi.Entities;

namespace SoccerApp.WebApi.Context
{
    public class ApiContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-BUFC83E\\SQLEXPRESS;initial catalog=SoccerApiDB;integrated security=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Team>()
              .HasIndex(t => t.ShortName)
              .IsUnique();

            mb.Entity<Team>()
              .HasOne(t => t.Stadium)
              .WithMany(s => s.Teams)
              .HasForeignKey(t => t.StadiumId)
              .OnDelete(DeleteBehavior.SetNull);

            // Aynı tabloya iki FK → cascade kapatılmalı
            mb.Entity<Match>()
              .HasOne(m => m.HomeTeam)
              .WithMany(t => t.HomeMatches)
              .HasForeignKey(m => m.HomeTeamId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Match>()
              .HasOne(m => m.AwayTeam)
              .WithMany(t => t.AwayMatches)
              .HasForeignKey(m => m.AwayTeamId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Match>()
              .HasIndex(m => new { m.WeekId, m.HomeTeamId, m.AwayTeamId })
              .IsUnique();

            // Maç silinince istatistik ve olaylar da silinsin
            mb.Entity<MatchStatistic>()
              .HasOne(s => s.Match)
              .WithOne(m => m.MatchStatistic)
              .HasForeignKey<MatchStatistic>(s => s.MatchId)
              .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<MatchEvent>()
              .HasOne(e => e.Team).WithMany()
              .HasForeignKey(e => e.TeamId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<MatchEvent>()
              .HasOne(e => e.Player).WithMany()
              .HasForeignKey(e => e.PlayerId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<MatchEvent>()
              .HasOne(e => e.PlayerIn).WithMany()
              .HasForeignKey(e => e.PlayerInId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<EventType>()
              .HasIndex(e => e.Code)
              .IsUnique();
        }

        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Week> Weeks { get; set; }
    }
}
