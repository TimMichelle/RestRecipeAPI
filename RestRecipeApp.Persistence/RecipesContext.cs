using Microsoft.EntityFrameworkCore;
using RecipesApp.Domain;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence;

public class RecipesContext : DbContext
{
    public RecipesContext(DbContextOptions<RecipesContext> options) : base(options)
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
}