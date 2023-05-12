using Evento.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Commands.Authentication
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public Role Role { get; set; }
    }
}
