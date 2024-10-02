using Microsoft.AspNetCore.Mvc;
using AlturCase.Core.Interfaces;
using AlturCase.Core.Dto.Request.User;

namespace AlturCase.Web.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (await _authService.RegisterUser(registerUserDto))
            {
                return Ok($"User {registerUserDto.Email} created");
            }

            return BadRequest("User could not created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = await _authService.Login(loginUserDto);
            if (identityUser == null)
            {
                return Unauthorized("Wrong Email or Password!");
            }

            try
            {
                var tokenString = _authService.GenerateTokenString(identityUser, loginUserDto);
                Response.Headers.Add("Authorization", $"Bearer {tokenString}");
                Console.WriteLine($"TOKEN: {tokenString}");
                return Ok($"User {loginUserDto.Email} logged in. Success!");
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, $"Token could not created! {ex.Message}");
            }
        }
    }
}
