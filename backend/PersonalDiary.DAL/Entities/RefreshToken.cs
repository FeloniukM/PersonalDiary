using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.DAL.Entities
{
    public class RefreshToken : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        private const int DAYS_TO_EXPIRE = 5;

        public RefreshToken()
        {
            Expires = DateTime.UtcNow.AddDays(DAYS_TO_EXPIRE);
        }

        public string Token { get; set; } = null!;
        public DateTime Expires { get; private set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsActive => DateTime.UtcNow <= Expires;
    }
}
