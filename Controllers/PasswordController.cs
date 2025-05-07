using Microsoft.AspNetCore.Mvc;
using OnlyMyKeyBackend.Requests;
using OnlyMyKeyBackend.Services;

namespace OnlyMyKeyBackend.Controllers
{
    [ApiController]
    [Route("api/password")]
    public class PasswordController(PasswordService passwordService, UserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPasswords()
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return Unauthorized();

            var user = await userService.GetByAuthTokenAsync(token.ToString());

            var passwords = await passwordService.GetByUserIdAsync(user.Id);

            return Ok(passwords);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] CreatePassword request)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return Unauthorized();

            var user = await userService.GetByAuthTokenAsync(token.ToString());

            if (user == null)
                return Unauthorized();

            await passwordService.AddNewPasswordToList(user.Id, request);

            return Ok();
        }

        [HttpDelete("{num:int}")]
        public async Task<IActionResult> DeletePassword([FromRoute] int num)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token.ToString()))
                return Unauthorized();

            var user = await userService.GetByAuthTokenAsync(token.ToString());

            if (user == null)
                return Unauthorized();

            await passwordService.DeleteByIndexAsync(user.Id, num);

            return Ok();
        }
    }
}
