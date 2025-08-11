using JugendLeagueWeb.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JugendLeagueWeb.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<TeamPlayer> TeamPlayers => Set<TeamPlayer>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Tournament
            b.Entity<Tournament>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.PublicId).IsUnique();

                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Location).HasMaxLength(200);
                e.Property(x => x.OptimalTeamSize).IsRequired();
                e.Property(x => x.MaximalTeamSize).IsRequired();

                e.Property(x => x.RowVersion).IsRowVersion();

                e.HasOne(x => x.ResponsibleUser)
                    .WithMany(u => u.ResponsibleTournaments)
                    .HasForeignKey(x => x.ResponsibleUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Group (optional)
            b.Entity<Group>(e =>
            {
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();

                e.HasOne(x => x.Tournament)
                    .WithMany(t => t.Groups)
                    .HasForeignKey(x => x.TournamentId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => new { x.TournamentId, x.Name }).IsUnique();
            });

            // Team
            b.Entity<Team>(e =>
            {
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();

                e.HasOne(t => t.Tournament)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TournamentId)
                .OnDelete(DeleteBehavior.Restrict); // ← statt Cascade

                e.HasOne(x => x.Group)
                    .WithMany(g => g.Teams)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Teamnamen pro Turnier eindeutig
                e.HasIndex(x => new { x.TournamentId, x.Name }).IsUnique();
            });

            // User
            b.Entity<User>(e =>
            {
                e.Property(x => x.Firstname).HasMaxLength(100).IsRequired();
                e.Property(x => x.Lastname).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(256);

                // E-Mail optional, aber wenn gesetzt, eindeutig
                e.HasIndex(x => x.Email)
                    .IsUnique()
                    .HasFilter("[Email] IS NOT NULL");
            });

            // TeamPlayer (Join)
            b.Entity<TeamPlayer>(e =>
            {
                e.HasKey(tp => new { tp.TeamId, tp.UserId });

                e.HasOne(tp => tp.Team)
                    .WithMany(t => t.TeamPlayers)
                    .HasForeignKey(tp => tp.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Löschen eines Users soll nicht alle Zuordnungen "wegsprengen" -> Restrict
                e.HasOne(tp => tp.User)
                    .WithMany(u => u.TeamPlayers)
                    .HasForeignKey(tp => tp.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.Property(tp => tp.Role).HasMaxLength(100);
            });
        }
    }
}
