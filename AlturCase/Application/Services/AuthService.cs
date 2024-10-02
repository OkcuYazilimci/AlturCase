using AlturCase.Core.Interfaces;
using AlturCase.Core.Dto.Request.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlturCase.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config, IJwtService jwtService)
        {
            _userManager = userManager;
            _config = config;
            _jwtService = jwtService;
        }

        public async Task<IdentityUser> Login(LoginUserDto loginUserDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (identityUser == null) {
                return null;
            }

            if(!await _userManager.CheckPasswordAsync(identityUser, loginUserDto.Password)) { return null; }
            
            return identityUser;
        }

        public async Task<bool> RegisterUser(RegisterUserDto registerUserDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUserDto.Email,
                Email = registerUserDto.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, registerUserDto.Password);
            return result.Succeeded;
        }

        public string GenerateTokenString(IdentityUser user, LoginUserDto loginUserDto)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, loginUserDto.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            };

            return _jwtService.GenerateToken(claims);
        }   
    }
}
