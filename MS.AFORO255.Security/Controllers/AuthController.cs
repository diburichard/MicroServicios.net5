using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.Security.Services;

namespace MS.AFORO255.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public AuthController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public IActionResult Get()
        {
            return Ok(_accessService.GetAll());
        }
    }
}
