using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class RecipeStepTestBuilder : Faker<RecipeStep>
{
    public RecipeStepTestBuilder(int recipeId)
    {
        CustomInstantiator(f => new RecipeStep()
        {
            StepNumber = f.Random.Int(1,6),
            Description = f.Lorem.Sentences(),
            RecipeId = recipeId,
        });
    }
}