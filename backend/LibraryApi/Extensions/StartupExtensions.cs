using LibraryApi.Application.Extensions;
using LibraryApi.Data;
using LibraryApi.Data.Extensions;
using LibraryApi.Integrations.Extensions;
using Microsoft.EntityFrameworkCore;

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

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}