using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace JugendLeagueWeb.Data
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Accounts.ToListAsync<User>();
        }
        public async Task<User> GetUser(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Accounts.SingleOrDefaultAsync<User>(u => u.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
        public async Task<bool> CreateUser(User user)
        {
            await _context.Accounts.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUser(User user)
        {
            _context.Accounts.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUser(User user)
        {
            _context.Accounts.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
