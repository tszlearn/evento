using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Settings
{
    public class JwtSettings
    {
        public const string SectionName = "jwt";


        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
