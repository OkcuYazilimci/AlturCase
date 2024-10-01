using AlturCase.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Http;
using AlturCase.Models.Dto.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using AlturCase.Services.Abstract;

namespace AlturCase.Controllers
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
                return Unauthorized("Invalid credentials.");
            }

            var tokenString = _authService.GenerateTokenString(identityUser, loginUserDto);
                return Ok($"User {tokenString} logged in. Success!");
            
            return BadRequest("Email or password is wrong!");
        }
    }
}
