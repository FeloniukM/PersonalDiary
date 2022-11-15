using Microsoft.IdentityModel.Tokens;

namespace PersonalDiary.Common.Auth
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; } = null!;
    }
}
