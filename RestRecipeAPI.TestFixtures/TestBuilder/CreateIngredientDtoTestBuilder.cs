using Bogus;
using RecipesApp.Domain;
using RestRecipeApp.Core.RequestDto.Recipe;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateIngredientDtoTestBuilder : Faker<CreateIngredientDto>
{
    public CreateIngredientDtoTestBuilder(int? productId = null)
    {
        CustomInstantiator(f => new CreateIngredientDto(
            productId ?? 1, f.Random.Float(1, 100),
            f.PickRandom<UnitOfMeasurement>()));
    }
    
}