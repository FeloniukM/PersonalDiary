namespace PersonalDiary.Common.DTO.Image
{
    public class ImageInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image_id { get; set; } = null!;
        public string Permalink_url { get; set; } = null!;
        public string Thumb_url { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
