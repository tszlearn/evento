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
        Task RegisterAsync(string username, string password, string email, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
        Task ForgotPassword(string email);
        Task ResetPassword(string token, string password);
        Task PasswordTokenValidate();
    }
}
