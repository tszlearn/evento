using Evento.Core.Domain;

namespace Evento.Infrastructure.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public Role Role { get; set; }
        public long Expires { get; set; }
    }
}
