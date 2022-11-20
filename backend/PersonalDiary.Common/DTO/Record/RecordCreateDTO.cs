using Microsoft.AspNetCore.Http;

namespace PersonalDiary.Common.DTO.Record
{
    public class RecordCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
