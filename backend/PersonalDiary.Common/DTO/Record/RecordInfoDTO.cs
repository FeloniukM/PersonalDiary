using PersonalDiary.Common.DTO.Image;

namespace PersonalDiary.Common.DTO.Record
{
    public class RecordInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public ImageInfoDTO? Image { get; set; }
    }
}
