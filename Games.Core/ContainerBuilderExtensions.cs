using Autofac;
using Games.Core.Services;
using Games.Core.Services.Registration;
using Games.Core.Services.Roles;
using Games.Core.Services.Users;

namespace Games.Core
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterCore(this ContainerBuilder builder)
        {
            builder.RegisterType<GameManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<RegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<RolesManagementService>().InstancePerLifetimeScope();
        }
    }
}
