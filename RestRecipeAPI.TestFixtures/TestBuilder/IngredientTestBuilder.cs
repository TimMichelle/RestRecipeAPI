using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public class IngredientTestBuilder : Faker<Ingredient>
{
    public IngredientTestBuilder(int productId, int recipeId)
    {
        CustomInstantiator(f => new Ingredient()
        {
            Amount = f.Random.Float(1, 100),
            ProductId = productId,
            UnitOfMeasurement = f.PickRandom<UnitOfMeasurement>(),
            RecipeId = recipeId,
        });
    }
}