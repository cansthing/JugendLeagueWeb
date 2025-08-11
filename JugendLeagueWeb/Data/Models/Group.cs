namespace JugendLeagueWeb.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
