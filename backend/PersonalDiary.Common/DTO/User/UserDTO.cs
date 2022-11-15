namespace PersonalDiary.Common.DTO.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
