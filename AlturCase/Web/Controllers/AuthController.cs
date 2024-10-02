using Microsoft.AspNetCore.Mvc;
using AlturCase.Core.Interfaces;
using AlturCase.Core.Dto.Request.User;

namespace AlturCase.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (await _authService.RegisterUser(registerUserDto))
            {
                return Ok($"User {registerUserDto.Email} created");
            }

            return BadRequest("User could not created");
        }

        [HttpPost("Login")]
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
                return Ok($"User {tokenString} logged in. Success!");
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, $"Token could not created! {ex.Message}");
            }
        }
    }
}
