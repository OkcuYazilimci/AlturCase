using AlturCase.Core.Dto.Request.User;
using Microsoft.AspNetCore.Identity;

namespace AlturCase.Core.Interfaces
{
    public interface IAuthService
    {
        public string GenerateTokenString(IdentityUser user, LoginUserDto loginUserDto);
        Task<IdentityUser> Login(LoginUserDto LoginUserDto);
        Task<bool> RegisterUser(RegisterUserDto registerUserDto);
    }
}