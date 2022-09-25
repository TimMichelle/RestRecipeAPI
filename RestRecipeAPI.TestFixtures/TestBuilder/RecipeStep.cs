using Bogus;
using RecipesApp.Domain;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public class RecipeStepTestBuilder : Faker<RecipeStep>
{
    public RecipeStepTestBuilder(int stepNumber)
    {
        CustomInstantiator(f => new RecipeStep()
        {
            StepNumber = stepNumber,
            Description = f.Lorem.Sentences()
        });
    }
}