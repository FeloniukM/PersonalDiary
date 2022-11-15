using AutoMapper;
using PersonalDiary.Common.DTO.User;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserRegisterDTO, User>();
        }
    }
}
