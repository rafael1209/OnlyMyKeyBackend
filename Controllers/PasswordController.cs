using Microsoft.AspNetCore.Mvc;
using OnlyMyKeyBackend.Requests;
using OnlyMyKeyBackend.Services;

namespace OnlyMyKeyBackend.Controllers
{
    [ApiController]
    [Route("api/password")]
    public class PasswordController(PasswordService passwordService, UserService userService) : Controller
    {
        private readonly PasswordService _passwordService = passwordService;
        private readonly UserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAllPasswords()
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return Unauthorized();

            var user = await _userService.GetByAuthTokenAsync(token.ToString());

            var passwords = await _passwordService.GetByUserIdAsync(user.Id);

            return Ok(passwords);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] CreatePassword request)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return Unauthorized();

            var user = await _userService.GetByAuthTokenAsync(token.ToString());

            if (user == null)
                return Unauthorized();

            await _passwordService.AddNewPasswordToList(user.Id, request);

            return Ok();
        }
    }
}
