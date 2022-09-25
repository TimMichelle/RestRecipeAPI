using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class RecipeTestBuilder : Faker<Recipe>
{
    public RecipeTestBuilder()
    {
        CustomInstantiator(f => new Recipe()
        {
            Name = f.Lorem.Word(),
            CookingTime = f.Random.Int(10, 90),
            TotalPersons = f.Random.Int(2, 10),
        });
    }
}