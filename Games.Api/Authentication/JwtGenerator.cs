using Games.Core.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Games.Api.Authentication
{
    public class JwtGenerator
    {
        private readonly AuthenticationOptions _options;

        public JwtGenerator(AuthenticationOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        internal string Generate(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.IssuerSigningKey);
            var expires = (DateTimeOffset.UtcNow + _options.TokenExpiration).UtcDateTime;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FirstName", user.FirstName.ToString()),
                    new Claim("LastName", user.LastName.ToString()),
                    new Claim("Roles", JsonConvert.SerializeObject(user.Roles.Select(r => r.Role.Name).ToList()))
                }),
                IssuedAt = DateTimeOffset.UtcNow.UtcDateTime,
                NotBefore = DateTimeOffset.UtcNow.UtcDateTime,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
