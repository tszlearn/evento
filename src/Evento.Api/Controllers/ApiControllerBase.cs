using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase: Controller
    {
        protected int? UserId => User?.Identity?.IsAuthenticated == true ? 
            int.Parse(User.Identity.Name) : 
            null;
    }
}
