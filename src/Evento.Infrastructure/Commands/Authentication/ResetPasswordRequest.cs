using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Commands.Authentication
{
    public class ResetPasswordRequest
    {
        public int Email { get; set; }
    }
}
