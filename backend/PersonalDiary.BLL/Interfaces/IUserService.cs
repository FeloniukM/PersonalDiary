using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(UserRegisterDTO userDto);
        Task InviteUser(UserEmailDTO userInviteDTO, Guid adminId);
        Task<UserInfoDTO> GetUserInfo(Guid userId);
        Task ChangeUserRole(UserEmailDTO userEmailDTO, Guid adminId);
    }
}
