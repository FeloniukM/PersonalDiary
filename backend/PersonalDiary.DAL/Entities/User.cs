using PersonalDiary.DAL.Entities.Abstractions;

namespace PersonalDiary.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public DateTime WhenDeleted { get; set; } 
        public string Salt { get; set; } = null!;

        public List<Record>? Records { get; set; }
    }
}
