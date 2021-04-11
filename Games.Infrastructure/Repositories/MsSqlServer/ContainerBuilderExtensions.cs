using Autofac;
using Autofac.Extensions.DependencyInjection;
using Games.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Infrastructure.Repositories.MsSqlServer
{
    internal static class ContainerBuilderExtensions
    {
        public static void RegisterMsSqlServerRespositories(this ContainerBuilder builder, IConfiguration configuration)
        {
            var options = configuration.GetValueOrDefault<MsSqlServerRepositoriesOptions>();
            var services = new ServiceCollection();

            services.AddDbContext<GamesDbContext>(op => op.UseSqlServer(options.ConnectionString), ServiceLifetime.Transient);
            builder.RegisterAssemblyTypes(typeof(MsSqlServerBaseRepository<>).Assembly)
                .AsClosedTypesOf(typeof(MsSqlServerBaseRepository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Populate(services);
        }
    }
}
