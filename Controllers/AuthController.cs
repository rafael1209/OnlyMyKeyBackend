using Microsoft.AspNetCore.Mvc;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Services;

namespace OnlyMyKeyBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IGoogleAuthService googleAuthService, UserService userService) : Controller
    {
        private readonly IGoogleAuthService _googleAuthService = googleAuthService;
        private readonly UserService _userService = userService;

        [HttpGet("url")]
        public IActionResult Index()
        {
            try
            {
                var url = _googleAuthService.GetGoogleAuthUrl();
                return Ok(new { url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error retrieving authorization URL.", details = ex.Message });
            }
        }

        [HttpGet("callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest(new { error = "Authorization code is required." });

            try
            {
                var result = await _googleAuthService.HandleGoogleCallbackAsync(code);

                if (result == null)
                    return BadRequest(new { error = "Invalid authorization code or failed to authenticate." });

                var user = await _userService.GetOrCreateUserByEmailAsync(result);

                if (user == null)
                    return BadRequest(new { error = "Db error" });

                return Ok(new {authToken = user.AuthToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error processing authorization.", details = ex.Message });
            }
        }
    }
}
