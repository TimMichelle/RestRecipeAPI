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
                b => b.MigrationsAssembly("RestRecipeApp.Persistence")));

        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();
        services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
        services.AddScoped<IShoppingListItemRepository, ShoppingListItemRepository>();
        return services;
    }
}