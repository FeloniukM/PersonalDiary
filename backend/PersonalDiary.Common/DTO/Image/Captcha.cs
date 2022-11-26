namespace PersonalDiary.Common.DTO.Image
{
    public class Captcha
    {
        public byte[] FileContents { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
