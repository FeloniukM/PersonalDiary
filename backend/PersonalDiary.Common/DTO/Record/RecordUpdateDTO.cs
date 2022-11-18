namespace PersonalDiary.Common.DTO.Record
{
    public class RecordUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
