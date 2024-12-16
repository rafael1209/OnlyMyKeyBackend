using Microsoft.AspNetCore.Mvc;
using OnlyMyKeyBackend.Services;

namespace OnlyMyKeyBackend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return BadRequest();

            var userDto = await userService.GetDtoByAuthTokenAsync(token.ToString());

            if (userDto == null)
                return Unauthorized(new { message = "Invalid or missing auth token" });

            return Ok(userDto);
        }
    }
}
