using Bogus;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class CreateRecipeStepTestBuilder  :Faker<CreateRecipeStepDto>
{
    public CreateRecipeStepTestBuilder()
    {
        CustomInstantiator(f => new CreateRecipeStepDto(f.Random.Int(1, 10), f.Lorem.Paragraph()));
    }
}