using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Persistence;

public static class ServiceCollectionPersistence
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecipesDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection") ?? string.Empty, 
                b => b.MigrationsAssembly("RestRecipeApp.DbMigrator")));

        services.AddScoped<IRecipeRepository, RecipeRepository>();
        return services;
    }
}