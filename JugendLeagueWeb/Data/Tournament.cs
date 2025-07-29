namespace JugendLeagueWeb.Data
{
    public class Tournament
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateTime { get; set; }
        public string? Location { get; set; }
        public User? Responsiblity { get; set; }
        public bool KO { get; set; }

    }
}
