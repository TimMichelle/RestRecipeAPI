using Bogus;
using RestRecipeApp.Core.RequestDto.Recipe;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateRecipeDtoTestBuilder : Faker<CreateRecipeDto>
{
    public CreateRecipeDtoTestBuilder()
    {
        CustomInstantiator(f => new CreateRecipeDto(
            f.Lorem.Slug(),
            f.Random.Int(1, 90),
            f.Random.Int(1, 10),
            new CreateIngredientDtoTestBuilder().Generate(4),
            new CreateRecipeStepTestBuilder().Generate(10)));
    }
}