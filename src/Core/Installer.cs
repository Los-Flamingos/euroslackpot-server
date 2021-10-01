﻿using Ardalis.GuardClauses;
using Core.ConfigurationOptions;
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
            Guard.Against.Null(configuration, nameof(configuration));

            serviceCollection.Configure<DatabaseConfigurationOptions>(configuration.GetSection(DatabaseConfigurationOptions.ConfigurationKey));
            return serviceCollection;
        }
    }
}
