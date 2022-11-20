using PersonalDiary.DAL.Entities.Abstractions;

namespace PersonalDiary.DAL.Entities
{
    public class Record : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public Image? Image { get; set; } = null;

        public User Author { get; set; } = null!;
        public Guid AuthorId { get; set; }
    }
}
