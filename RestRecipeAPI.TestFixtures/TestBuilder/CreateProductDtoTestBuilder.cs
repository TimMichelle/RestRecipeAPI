using Bogus;
using RestRecipeApp.Core.RequestDto.Recipe;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateProductDtoTestBuilder : Faker<CreateProductDto>
{
    public CreateProductDtoTestBuilder()
    {
        CustomInstantiator(faker => new CreateProductDto(faker.Lorem.Word()));
    }
}