using Bogus;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateProductDtoTestBuilder : Faker<CreateProductDto>
{
    public CreateProductDtoTestBuilder()
    {
        CustomInstantiator(faker => new CreateProductDto(faker.Lorem.Word()));
    }
}