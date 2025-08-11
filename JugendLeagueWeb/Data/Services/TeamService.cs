using JugendLeagueWeb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JugendLeagueWeb.Data.Services
{
    public class TeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context) => _context = context;

        public async Task<Team?> GetAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.TeamPlayers).ThenInclude(tp => tp.User)
                .Include(t => t.Tournament)
                .Include(t => t.Group)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Team>> GetByTournamentAsync(int tournamentId)
        {
            return await _context.Teams
                .Include(t => t.TeamPlayers).ThenInclude(tp => tp.User)
                .Where(t => t.TournamentId == tournamentId)
                .ToListAsync();
        }

        public async Task SaveAsync(Team team, IEnumerable<User> players)
        {
            if (team.Id == 0)
                _context.Teams.Add(team);
            else
                _context.Teams.Update(team);

            await _context.SaveChangesAsync();

            // Save TeamPlayers
            _context.TeamPlayers.RemoveRange(_context.TeamPlayers.Where(tp => tp.TeamId == team.Id));
            foreach (var player in players)
            {
                _context.TeamPlayers.Add(new TeamPlayer
                {
                    TeamId = team.Id,
                    UserId = player.Id
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team is null) return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
