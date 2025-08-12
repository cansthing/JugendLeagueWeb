using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace JugendLeagueWeb.Data.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;
        public DateTimeOffset StartAt { get; set; } // statt DateTime (Zeitzonenfreundlicher)
        public string? Location { get; set; }

        public int? ResponsibleUserId { get; set; }
        public User? ResponsibleUser { get; set; }

        public bool IsKnockout { get; set; } // statt KO
        public int OptimalTeamSize { get; set; }
        public int MaximalTeamSize { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public ICollection<Group> Groups { get; set; } = new HashSet<Group>();

        public byte[]? RowVersion { get; set; } = null!; // Concurrency-Token
    }
}
