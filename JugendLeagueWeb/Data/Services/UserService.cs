using JugendLeagueWeb.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace JugendLeagueWeb.Data.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) => _context = context;

        public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<int> SaveAsync(User user)
        {
            if (user.Id == 0)
                _context.Users.Add(user);
            else
                _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}