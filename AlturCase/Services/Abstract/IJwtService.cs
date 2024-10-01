using System.Security.Claims;

namespace AlturCase.Services.Abstract
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}