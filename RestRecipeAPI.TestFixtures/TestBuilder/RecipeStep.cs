using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public class RecipeStepTestBuilder : Faker<RecipeStep>
{
    public RecipeStepTestBuilder()
    {
        CustomInstantiator(f => new RecipeStep()
        {
            StepNumber = f.Random.Int(1,6),
            Description = f.Lorem.Sentences()
        });
    }
}