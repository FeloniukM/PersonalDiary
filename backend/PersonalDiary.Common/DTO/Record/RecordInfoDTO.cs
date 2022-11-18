namespace PersonalDiary.Common.DTO.Record
{
    public class RecordInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }
}
