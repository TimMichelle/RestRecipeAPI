using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.Domain;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence;

public class RecipesDbContext : DbContext
{
    public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Ingredient>()
            .Property(e => e.UnitOfMeasurement)
            .HasConversion(
                v => v.ToString(),
                v => (UnitOfMeasurement) Enum.Parse(typeof(UnitOfMeasurement), v));
    }

    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;
    public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
}