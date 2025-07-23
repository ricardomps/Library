using LibraryApi.Application.Connectors;
using LibraryApi.Application.Interfaces.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Application.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddConnectors(this IServiceCollection services)
    {
        services.AddScoped<IBookConnector, BookConnector>();
        return services;
    }
}