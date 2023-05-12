using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Commands.Authentication
{
    public class ForgotPasswordRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
