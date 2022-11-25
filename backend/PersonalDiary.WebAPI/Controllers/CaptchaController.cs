using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Interfaces;

namespace PersonalDiary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;

        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        [HttpGet]
        public IActionResult GetCaptcha()
        {
            var captcha = _captchaService.GetCaptcha(HttpContext);

            return Ok(captcha);
        }

        [HttpGet("verify/{answer}")]
        public IActionResult VerifyCaptcha(int answer)
        {
            var isCorrect = _captchaService.VerifyCapcha(HttpContext, answer);
            
            return Ok(isCorrect);
        }
    }
}
