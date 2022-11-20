using AutoMapper;
using PersonalDiary.Common.DTO.Image;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.MappingProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageInfoDTO>();
        }
    }
}
