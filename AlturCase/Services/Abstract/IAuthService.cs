using AlturCase.Areas.Identity.Pages.Account;
using AlturCase.Models.Dto.Request;

namespace AlturCase.Services.Abstract
{
    public interface IAuthService
    {
        Task<bool> Login(LoginUserDto LoginUserDto);
        Task<bool> RegisterUser(RegisterUserDto registerUserDto);
    }
}