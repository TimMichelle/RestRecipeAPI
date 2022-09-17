using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestRecipeApp.Db;

namespace Tests.RestRecipeApp;

public class RestRecipeAppWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<RecipesContext>));

            services.Remove(descriptor);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Test.json", false, true)
                .Build();

            services.AddDbContext<RecipesContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection") ?? throw new InvalidOperationException());
            });

        });
    }

}