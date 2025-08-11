using JugendLeagueWeb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JugendLeagueWeb.Data.Services
{
    public class GroupService
    {
        private readonly AppDbContext _context;

        public GroupService(AppDbContext context) => _context = context;

        public async Task<Group?> GetAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Teams).ThenInclude(t => t.TeamPlayers).ThenInclude(tp => tp.User)
                .Include(g => g.Tournament)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Group>> GetByTournamentAsync(int tournamentId)
        {
            return await _context.Groups
                .Include(g => g.Teams)
                .Where(g => g.TournamentId == tournamentId)
                .ToListAsync();
        }

        public async Task<int> SaveAsync(Group group)
        {
            if (group.Id == 0)
                _context.Groups.Add(group);
            else
                _context.Groups.Update(group);

            await _context.SaveChangesAsync();
            return group.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group is null) return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AssignTeamAsync(int groupId, int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team is null) return;

            team.GroupId = groupId;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team is null) return;

            team.GroupId = null;
            await _context.SaveChangesAsync();
        }
    }
}
