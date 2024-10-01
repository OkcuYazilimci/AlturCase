using AlturCase.Models.Dto.Request;
using Microsoft.AspNetCore.Identity;

namespace AlturCase.Services.Abstract
{
    public interface IAuthService
    {
        public string GenerateTokenString(IdentityUser user, LoginUserDto loginUserDto);
        Task<IdentityUser> Login(LoginUserDto LoginUserDto);
        Task<bool> RegisterUser(RegisterUserDto registerUserDto);
    }
}