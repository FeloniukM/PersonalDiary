using PersonalDiary.Common.DTO.Auth;

namespace PersonalDiary.Common.DTO.User
{
    public class AuthUserDTO
    {
        public UserDTO User { get; set; } = null!;
        public AccessTokenDTO Token { get; set; } = null!;
    }
}
