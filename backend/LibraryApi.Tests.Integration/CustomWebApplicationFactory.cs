using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Tests.Integration.Fakes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Tests.Integration;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("TEST");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
            { "ConnectionStrings:Library", "TestConnectionString" },
            });
        })
        .ConfigureTestServices(services =>
        {
            services.AddScoped<IBookRepository, FakeBookRepository>();
        });
    }
}