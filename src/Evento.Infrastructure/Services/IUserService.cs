using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(int userId);
        Task PasswordTokenValidate();
    }
}
