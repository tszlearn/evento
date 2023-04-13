using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Evento.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventoContext _context;

        public UserRepository(EventoContext context) 
        {
            _context = context;
        }


        public async Task<User?> GetAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(x => x.ID == id);

        public async Task<User?> GetAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(x => string.Equals(x.Email, email.ToLowerInvariant()));

        public async Task<User?> GetByTokenAsync(string token)
            => await _context.Users.FirstOrDefaultAsync(x => string.Equals(x.ResetPasswordToken, token));

        public async Task<ICollection<User>> GetResetPasswordUsers()
        {
            return await _context.Users.Where(x=> !string.IsNullOrWhiteSpace(x.ResetPasswordToken)).ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<User> updateUsers)
        {
            _context.Users.UpdateRange(updateUsers);
            await _context.SaveChangesAsync();
        }
    }
}
