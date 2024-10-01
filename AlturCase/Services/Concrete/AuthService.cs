using AlturCase.Models.Dto.Request;
using AlturCase.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AlturCase.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginUserDto loginUserDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (identityUser == null) {
                return false;
            }

            var result = await _userManager.CheckPasswordAsync(identityUser, loginUserDto.Password);
            return true;
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
    }
}
