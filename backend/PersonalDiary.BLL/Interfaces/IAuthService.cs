using PersonalDiary.Common.DTO.Auth;
using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthUserDTO> Authorize(UserLoginDTO userDto);
        Task<AccessTokenDTO> GenerateAccessToken(Guid userId, string nickname, string email);
        Task<AccessTokenDTO> RefreshToken(RefreshTokenDTO dto);
        Task RevokeRefreshToken(string refreshToken, Guid userId);
    }
}
