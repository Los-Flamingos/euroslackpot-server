using Core.Contracts;
using Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class Installer
    {
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPlayerService, PlayerService>();
            serviceCollection.AddScoped<IRowService, RowService>();
            return serviceCollection;
        }
    }
}
