using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace JugendLeagueWeb.Data.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;

        public int? GroupId { get; set; } // optional
        public Group? Group { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new HashSet<TeamPlayer>();
    }
}