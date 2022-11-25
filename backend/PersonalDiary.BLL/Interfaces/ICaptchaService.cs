using Microsoft.AspNetCore.Http;
using PersonalDiary.Common.DTO.Image;

namespace PersonalDiary.BLL.Interfaces
{
    public interface ICaptchaService
    {
        Captcha GetCaptcha(HttpContext httpContext);
        bool VerifyCapcha(HttpContext httpContext, int answer);
    }
}
