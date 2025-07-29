using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JugendLeagueWeb.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<User> Accounts { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}
