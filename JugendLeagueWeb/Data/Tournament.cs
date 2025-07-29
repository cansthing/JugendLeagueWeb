using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
namespace JugendLeagueWeb.Data
{
    public class Tournament
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateTime { get; set; }
        public string? Location { get; set; }
        public int ResponsiblityId { get; set; }
        public bool KO { get; set; }

    }
}
