using Microsoft.AspNetCore.Http;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IUploadService
    {
        Task<Image> UploadImage(IFormFile image);
        Task DeleteImage(string imageId);
    }
}
