
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JugendLeagueWeb.Data
{
    public class TournamentService
    {
        private readonly AppDbContext _context;

        public TournamentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tournament>> GetAllTournaments()
        {
            return await _context.Tournaments.ToListAsync<Tournament>();
        }
        public async Task<Tournament> GetTournament()
        {
            return await _context.Tournaments.SingleOrDefaultAsync<Tournament>();
        }
        public async Task<bool> CreateTournament(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateTournament(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTournament(Tournament tournament)
        {
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
