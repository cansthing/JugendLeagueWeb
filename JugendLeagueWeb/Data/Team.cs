


namespace JugendLeagueWeb.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<User>? Players { get; set; }
        public int TournamentId { get; set; }
        public int GroupId { get; set; }
    }
}