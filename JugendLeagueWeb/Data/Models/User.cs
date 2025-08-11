using System.ComponentModel.DataAnnotations.Schema;

namespace JugendLeagueWeb.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime Birthday { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new HashSet<TeamPlayer>();
        public ICollection<Tournament> ResponsibleTournaments { get; set; } = new HashSet<Tournament>();

        public override string ToString() => $"{Firstname} {Lastname}";
    }
}
