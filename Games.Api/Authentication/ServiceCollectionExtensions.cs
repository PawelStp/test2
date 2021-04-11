using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Api.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfigurationSection authenticationConfiguration)
        {
            var options = authenticationConfiguration.Get<AuthenticationOptions>();

        }
    }
}
