using Autofac;
using Games.Common;
using Microsoft.Extensions.Configuration;

namespace Games.Api.Authentication
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAuthentication(this ContainerBuilder builder, IConfiguration configuration)
        {
            var options = configuration.GetValueOrDefault<AuthenticationOptions>();

            builder.RegisterInstance(options);
            builder.RegisterType<JwtGenerator>();
        }
    }
}
