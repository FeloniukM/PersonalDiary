using Microsoft.AspNetCore.Http;
using PersonalDiary.BLL.Exeptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.DAL.Entities;
using System.Net.Http.Json;

namespace PersonalDiary.BLL.Service
{
    internal class UploadService : IUploadService
    {
        private readonly HttpClient _httpClient;

        public UploadService(HttpClient httpService)
        {
            _httpClient = httpService; 
        }

        public async Task<Image> UploadImage(IFormFile file)
        {
            var content = new MultipartFormDataContent
            {
                { new StreamContent(file.OpenReadStream()), "imagedata", file.FileName }
            };

            var responce = await _httpClient.PostAsync(BuildUrl("https://upload.gyazo.com/api/upload"), content);

            var body = await responce.Content.ReadFromJsonAsync(typeof(Image));

            if(body == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Response body empty");
            }

            return (Image)body;
        }

        public async Task DeleteImage(string imageId)
        {
            await _httpClient.DeleteAsync(BuildUrl($"https://api.gyazo.com/api/images/{imageId}"));
        }

        private string BuildUrl(string url)
        {
            return $"{url}?access_token=-vudl7THwMSy0SUU7HTRWblryvYpgN7H_FkZS1VTURg";
        }
    }
}
