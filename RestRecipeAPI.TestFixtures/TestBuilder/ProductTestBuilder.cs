using Bogus;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeAPI.TestFixtures.TestBuilder;

public sealed class ProductTestBuilder: Faker<Product>
{
    public ProductTestBuilder()
    {
        CustomInstantiator(f => new Product()
        {
            Name = f.Lorem.Word()
        });
    }
}