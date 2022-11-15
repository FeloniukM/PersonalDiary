using Microsoft.IdentityModel.Tokens;
using PersonalDiary.Common.Auth;
using System.Security.Claims;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateAccessToken(Guid id, string nickname, string email);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
        Guid GetUserIdFromToken(string accessToken, string signingKey);
    }
}
