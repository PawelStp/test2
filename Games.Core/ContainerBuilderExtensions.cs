using Autofac;
using Games.Core.Services;
using Games.Core.Services.Registration;
using Microsoft.Extensions.Configuration;

namespace Games.Core
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterCore(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<GameManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationService>().InstancePerLifetimeScope();
        }
    }
}
