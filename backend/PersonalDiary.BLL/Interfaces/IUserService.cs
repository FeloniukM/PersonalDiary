using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(UserRegisterDTO userDto);
    }
}
