using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Games.Api.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfigurationSection authenticationConfiguration)
        {
            var options = authenticationConfiguration.Get<AuthenticationOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
                 {
                     x.RequireHttpsMetadata = false;
                     x.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.IssuerSigningKey)),
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ClockSkew = TimeSpan.Zero
                     };
                 });
        }
    }
}
