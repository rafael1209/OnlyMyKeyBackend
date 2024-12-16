using Microsoft.AspNetCore.Mvc;

namespace OnlyMyKeyBackend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpGet("url")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
