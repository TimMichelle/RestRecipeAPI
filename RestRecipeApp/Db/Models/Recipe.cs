
namespace RecipesApp.Domain;

public class Recipe
{
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public int CookingTime { get; set; }
    public int TotalPersons { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<RecipeStep> Steps { get; set; } = new();
}