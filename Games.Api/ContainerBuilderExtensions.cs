using Autofac;
using Games.Api.Authentication;
using Microsoft.Extensions.Configuration;

namespace Games.Api
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterApi(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterAuthentication(configuration.GetSection("Authentication"));
            builder.RegisterType<HandleExceptionsMiddleware>().AsSelf().AsImplementedInterfaces();
        }
    }
}
