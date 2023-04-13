using Evento.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(int id);
        Task<User?> GetAsync(string email);
        Task<User?> GetByTokenAsync(string token);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<ICollection<User>> GetResetPasswordUsers();
        Task UpdateRangeAsync(IEnumerable<User> updateUsers);
    }
}
