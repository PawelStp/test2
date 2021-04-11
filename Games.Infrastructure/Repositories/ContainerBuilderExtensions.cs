using Autofac;
using Games.Infrastructure.Repositories.MsSqlServer;
using Microsoft.Extensions.Configuration;
using System;

namespace Games.Infrastructure.Repositories
{
    internal static class ContainerBuilderExtensions
    {
        public static void RegisterRepositories(this ContainerBuilder builder, IConfiguration configuration)
        {
            var type = configuration.GetValue<RepositoryType>(nameof(RepositoryOptions.Type));

            switch (type)
            {
                case RepositoryType.MsSqlServer:
                    builder.RegisterMsSqlServerRespositories(configuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
