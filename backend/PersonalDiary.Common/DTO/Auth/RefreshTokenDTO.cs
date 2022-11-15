using System.Text.Json.Serialization;

namespace PersonalDiary.Common.DTO.Auth
{
    public sealed class RefreshTokenDTO
    {
        public RefreshTokenDTO()
        {
            var key = Environment.GetEnvironmentVariable("SecretJWTKey");

            if(key == null)
                throw new ArgumentNullException(nameof(key));

            SigningKey = key;
        }

        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        [JsonIgnore]
        public string SigningKey { get; private set; }
    }
}
