using Microsoft.Extensions.Configuration;


namespace Games.Common
{
    public static class ConfigurationExtensions
    {
        public static T GetValueOrDefault<T>(this IConfiguration configuration) where T : new()
        {
            return configuration.Get<T>() ?? new T();
        }
    }
}
