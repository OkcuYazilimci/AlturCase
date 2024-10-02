using System.Security.Claims;

namespace AlturCase.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}