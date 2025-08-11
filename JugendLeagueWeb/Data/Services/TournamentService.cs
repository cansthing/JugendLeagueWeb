using JugendLeagueWeb.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JugendLeagueWeb.Data.Services
{
    public class TournamentService
    {
        private readonly AppDbContext _context;

        public TournamentService(AppDbContext context) => _context = context;

        // 📥 Tournament speichern (neu oder aktualisiert)
        public async Task<int> SaveAsync(Tournament tournament)
        {
            if (tournament.Id == 0)
                _context.Tournaments.Add(tournament);
            else
                _context.Tournaments.Update(tournament);

            await _context.SaveChangesAsync();
            return tournament.Id;
        }

        // 📤 Einzelnes Turnier mit Teams & Spielern laden
        public async Task<Tournament?> GetByPublicIdAsync(Guid publicId)
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                    .ThenInclude(team => team.TeamPlayers)
                        .ThenInclude(tp => tp.User)
                .Include(t => t.ResponsibleUser)
                .Include(t => t.Groups)
                    .ThenInclude(g => g.Teams)
                .SingleOrDefaultAsync(t => t.PublicId == publicId);
        }

        // 📄 Alle Turniere inkl. Navigationsdaten
        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments
                .Include(t => t.ResponsibleUser)
                .Include(t => t.Teams)
                .ToListAsync();
        }

        // 🗑️ Turnier samt abhängiger Daten löschen
        public async Task<bool> DeleteAsync(int tournamentId)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Teams)
                    .ThenInclude(t => t.TeamPlayers)
                .Include(t => t.Groups)
                    .ThenInclude(g => g.Teams)
                        .ThenInclude(t => t.TeamPlayers)
                .FirstOrDefaultAsync(t => t.Id == tournamentId);

            if (tournament is null) return false;

            // Teams direkt am Turnier
            foreach (var team in tournament.Teams)
                _context.TeamPlayers.RemoveRange(team.TeamPlayers);
            _context.Teams.RemoveRange(tournament.Teams);

            // Teams in Gruppen
            foreach (var group in tournament.Groups)
            {
                foreach (var team in group.Teams)
                    _context.TeamPlayers.RemoveRange(team.TeamPlayers);

                _context.Teams.RemoveRange(group.Teams);
            }

            _context.Groups.RemoveRange(tournament.Groups);
            _context.Tournaments.Remove(tournament);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}