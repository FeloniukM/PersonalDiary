namespace PersonalDiary.BLL.Exeptions
{
    public sealed class InvalidTokenException : Exception
    {
        public InvalidTokenException(string tokenName) : base($"Invalid {tokenName} token.") 
        {
            
        }
    }
}
