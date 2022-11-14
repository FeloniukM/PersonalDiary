using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.DAL.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public bool IsDelete { get; set; } = false;

        public List<Record>? Records { get; set; }
    }
}
