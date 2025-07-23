using LibraryApi.Application.Extensions;
using LibraryApi.Data.Extensions;
using LibraryApi.Integrations.Extensions;

namespace LibraryApi.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Library");
        services.AddDatabase(connectionString);
        services.AddRepositories();
        services.AddConnectors();
        return services;
    }
}