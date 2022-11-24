namespace PersonalDiary.DAL.Encryptions
{
    public class EncryptionOptions
    {
        public byte[] Key { get; set; } = null!;
        public byte[] IV { get; set; } = null!;
    }
}
