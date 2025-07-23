using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Integrations.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Integrations.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }
}