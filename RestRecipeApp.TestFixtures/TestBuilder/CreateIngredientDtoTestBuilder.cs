using Bogus;
using RestRecipeApp.Core.Domain;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateIngredientDtoTestBuilder : Faker<CreateIngredientDto>
{
    public CreateIngredientDtoTestBuilder(int? recipeId = null)
    {
        CustomInstantiator(f => new CreateIngredientDto(
            new CreateProductDtoTestBuilder().Generate(), f.Random.Float(1, 100),
            f.PickRandom<UnitOfMeasurement>(), recipeId ?? null));
    }
    
}