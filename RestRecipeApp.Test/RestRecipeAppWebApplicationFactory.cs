using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestRecipeApp.Db;

namespace Tests.RestRecipeApp;

public class RestRecipeAppWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<RecipesContext>));

            services.Remove(descriptor);

            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath)
                .Build();

            
            services.AddDbContext<RecipesContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
            });
        });
    }
}