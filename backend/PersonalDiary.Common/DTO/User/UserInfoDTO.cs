namespace PersonalDiary.Common.DTO.User
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
    }
}
