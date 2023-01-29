using Bogus;
using RecipesApp.Domain;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateIngredientDtoTestBuilder : Faker<CreateIngredientDto>
{
    public CreateIngredientDtoTestBuilder(int recipeId)
    {
        CustomInstantiator(f => new CreateIngredientDto(recipeId,
            new CreateProductDtoTestBuilder().Generate(), f.Random.Float(1, 100),
            f.PickRandom<UnitOfMeasurement>()));
    }
    
}