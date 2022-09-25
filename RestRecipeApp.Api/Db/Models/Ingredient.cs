namespace RecipesApp.Domain;

public class Ingredient
{
    public int IngredientId { get; set; }
    public int ProductId { get; set; }
    public int RecipeId { get; set; }
    public Product Product { get; set; }
    public float Amount { get; set; }
    public UnitOfMeasurement UnitOfMeasurement { get; set; }
}