using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public class IngredientTestBuilder : Faker<Ingredient>
{
    public IngredientTestBuilder(int ProductId)
    {
        CustomInstantiator(f => new Ingredient()
        {
            Amount = f.Random.Float(1, 100),
            ProductId = ProductId,
            UnitOfMeasurement = f.PickRandom<UnitOfMeasurement>()
        });
    }
}