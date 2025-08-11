namespace JugendLeagueWeb.Data.Models
{
    public class TeamPlayer
    {
        // Composite Key: TeamId + UserId
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Optional: Payload-Felder (falls nützlich)
        public string? Role { get; set; }
        public DateTimeOffset? JoinedAt { get; set; }
    }
}
