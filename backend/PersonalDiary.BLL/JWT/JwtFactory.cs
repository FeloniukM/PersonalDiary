using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalDiary.BLL.Exeptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.Auth;
using PersonalDiary.Common.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PersonalDiary.BLL.JWT
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<AccessToken> GenerateAccessToken(Guid id, string nickname, string email)
        {
            var identity = GenerateClaimsIdentity(id, nickname);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, nickname),
                 new Claim(JwtRegisteredClaimNames.Email, email),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst("id")
             };

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);

            return new AccessToken(_jwtSecurityTokenHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(SecurityHelper.GetRandomBytes());
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
        {
            return ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false
            });
        }

        public Guid GetUserIdFromToken(string accessToken, string signingKey)
        {
            var claimsPrincipal = GetPrincipalFromToken(accessToken, signingKey);

            if (claimsPrincipal == null)
            {
                throw new InvalidTokenException("access");
            }

            return Guid.Parse(claimsPrincipal.Claims.First(c => c.Type == "id").Value);
        }

        private ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static ClaimsIdentity GenerateClaimsIdentity(Guid id, string nickname)
        {
            return new ClaimsIdentity(new GenericIdentity(nickname, "Token"), new[]
            {
                new Claim("id", id.ToString())
            });
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                          new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                         .TotalSeconds);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));   
            }

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
