using Bogus;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateRecipeDtoTestBuilder : Faker<CreateRecipeDto>
{
    public CreateRecipeDtoTestBuilder(List<CreateIngredientDto>? ingredients = null)
    {
        CustomInstantiator(f => new CreateRecipeDto(
            f.Lorem.Slug(),
            f.Random.Int(1, 90),
            f.Random.Int(1, 10),
            ingredients ?? new List<CreateIngredientDto>(),
            new List<CreateRecipeStepDto>()));
    }
}