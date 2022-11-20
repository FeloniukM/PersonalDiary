using PersonalDiary.DAL.Entities.Abstractions;

namespace PersonalDiary.DAL.Entities
{
    public class Image : BaseEntity
    {
        public string Image_id { get; set; } = null!;
        public string Permalink_url { get; set; } = null!;
        public string Thumb_url { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
