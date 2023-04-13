using Evento.Core.Domain;
using Evento.Infrastructure.DTO;

namespace Evento.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(int userId, Role role);
    }
}
