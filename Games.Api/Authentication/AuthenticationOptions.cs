using System;

namespace Games.Api.Authentication
{
    public class AuthenticationOptions
    {
        public TimeSpan TokenExpiration { get; set; } = TimeSpan.FromMinutes(60);
        public string IssuerSigningKey { get; set; }
    }
}
