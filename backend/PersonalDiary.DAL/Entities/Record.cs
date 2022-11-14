using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.DAL.Entities
{
    public class Record : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        public User Author { get; set; } = null!;
        public int AuthorId { get; set; }
    }
}
