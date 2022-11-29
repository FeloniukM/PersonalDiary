using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PersonalDiary.BLL.Exceptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Image;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Json;
using Image = PersonalDiary.DAL.Entities.Image;

namespace PersonalDiary.BLL.Service
{
    internal class UploadService : IUploadService
    {
        public readonly ImageStorageOptions _imageStorageOptions;
        private readonly HttpClient _httpClient;

        public UploadService(IOptions<ImageStorageOptions> option, HttpClient httpService)
        {
            _imageStorageOptions = option.Value;
            _httpClient = httpService; 
        }

        public async Task<Image> UploadImage(IFormFile file)
        {
            var fileName = file.FileName;
            var content = new MultipartFormDataContent();
            var responce = new HttpResponseMessage();

            if (file.Length > 10000000)
            {
                var image = await ResizeImage(file);

                using(var stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);

                    content = new MultipartFormDataContent
                    {
                        { new StreamContent(stream), "imagedata", fileName }
                    };

                    responce = await _httpClient.PostAsync(BuildUrl("https://upload.gyazo.com/api/upload"), content);
                }
            }
            else
            {
                content = new MultipartFormDataContent
                {
                    { new StreamContent(file.OpenReadStream()), "imagedata", fileName }
                };

                responce = await _httpClient.PostAsync(BuildUrl("https://upload.gyazo.com/api/upload"), content);
            }

            var body = await responce.Content.ReadFromJsonAsync<Image>();

            if (body == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Response body empty");
            }

            return body;
        }

        public async Task DeleteImage(string imageId)
        {
            await _httpClient.DeleteAsync(BuildUrl($"https://api.gyazo.com/api/images/{imageId}"));
        }

        private string BuildUrl(string url)
        {
            return $"{url}?access_token={_imageStorageOptions.AccessToken}";
        }

        private async Task<Bitmap> ResizeImage(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                using (var img = System.Drawing.Image.FromStream(memoryStream))
                {
                    Bitmap bitmap = new Bitmap(img, new Size(1980, 1080));
                    return bitmap;
                }
            }
        }
    }
}
