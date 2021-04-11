using Autofac;
using Games.Infrastructure.Repositories;
using JsonNet.ContractResolvers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Games.Infrastructure
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterInfrastructure(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterRepositories(configuration.GetSection("Repositories"));
            builder.Register(c => CreateDefaultJsonSerializerSettings()).InstancePerDependency();
            JsonConvert.DefaultSettings = CreateDefaultJsonSerializerSettings;
        }

        private static JsonSerializerSettings CreateDefaultJsonSerializerSettings()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterAndCtorCamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-ddYHH:mm:ss.ffffffK",
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
            return serializerSettings;
        }
    }
}
