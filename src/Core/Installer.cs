using Core.ConfigurationOptions;
using Core.DatabaseEntities;
using Dapper.FluentMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class Installer
    {
        public static IServiceCollection AddCoreServices(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            // TODO: Rewrite logic using reflection to find all mapping definitions
            // instead of manually adding them here
            FluentMapper.Initialize(
                config =>
                {
                    config.AddMap(new PlayerMap());
                    config.AddMap(new NumberMap());
                    config.AddMap(new RowMap());
                });

            serviceCollection.Configure<DatabaseConfigurationOptions>(configuration.GetSection(DatabaseConfigurationOptions.ConfigurationKey));
            return serviceCollection;
        }
    }
}
