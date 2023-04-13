using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Commands.Users
{
    public class ResetPassword
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
