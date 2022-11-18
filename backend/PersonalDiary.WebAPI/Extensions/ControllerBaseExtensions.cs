using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Exeptions;
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
                throw new InvalidTokenException("access");
            }

            return Guid.Parse(claimsUserId);
        }
    }
}
