using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PersonalDiary.WebAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claimsUserId))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return Guid.Parse(claimsUserId);
        }
    }
}
