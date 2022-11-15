using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Exeptions;

namespace PersonalDiary.WebAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if (string.IsNullOrEmpty(claimsUserId))
            {
                throw new InvalidTokenException("access");
            }

            return Guid.Parse(claimsUserId);
        }
    }
}
