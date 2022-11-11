using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestRecipeApp.Persistence.Repositories;

namespace RestRecipeApp.Persistence;

public static class ServiceCollectionPersistence
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecipesContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection") ?? string.Empty));

        services.AddScoped<IRecipeRepository, RecipeRepository>();
        return services;
    }
}