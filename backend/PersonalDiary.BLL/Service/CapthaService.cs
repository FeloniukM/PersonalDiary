using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using PersonalDiary.BLL.Exceptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Image;
using System.Drawing;

namespace PersonalDiary.BLL.Service
{
    public class CaptchaService : ICaptchaService
    {
        private readonly IMemoryCache _cache;

        public CaptchaService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Captcha GetCaptcha(HttpContext httpContext)
        {
            string id = GenerateString(httpContext);

            int textSize = 10;
            var width = 125;
            var height = 20;

            string textToWrite = id;
            textToWrite = textToWrite.Replace("|", "\n");

            try
            {
                using (var stream = new MemoryStream())
                {
                    var image = new Bitmap(width, height);
                    var graphics = Graphics.FromImage(image);

                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    var font = new Font("Verdana", textSize, FontStyle.Regular);
                    var solidBrush = new SolidBrush(Color.White);

                    graphics.FillRectangle(solidBrush, 0, 0, width, height);
                    graphics.DrawString(textToWrite, font, Brushes.Blue, 2, 3);
                    graphics.DrawLine(new Pen(Color.Black, 1), new Point(1, 1), new Point(100, 50));

                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    return new Captcha()
                    {
                        FileContents = stream.ToArray(),
                        ContentType = "image/jpeg",
                        FileName = "Captcha"                 
                    };
                }
            }
            catch
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "Captcha not created");
            }
        }

        public bool VerifyCapcha(HttpContext httpContext, int answer)
        {
            var value = _cache.Get<int>($"captcha{httpContext.Connection.Id}");

            return answer == value;
        }

        private string GenerateString(HttpContext httpContext)
        {
            Random random = new Random();

            int a = random.Next(1, 100);
            int b = random.Next(1, 100);

            int res = a + b;

            var cacheOption = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1000))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);

            _cache.Set($"captcha{httpContext.Connection.Id}", res, cacheOption);

            return $"{a}+{b}";
        }
    }
}
