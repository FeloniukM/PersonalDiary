using PersonalDiary.DAL.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.DAL.Entities
{
    public class Record : BaseEntity
    {
        [Encrypted]
        public string Title { get; set; } = null!;
        [Encrypted]
        public string Text { get; set; } = null!;
        public Image? Image { get; set; } = null;

        public User Author { get; set; } = null!;
        public Guid AuthorId { get; set; }
    }
}
