using Microsoft.Extensions.DependencyInjection;
using RecipesApp.Domain;
using RestRecipeAPI.TestFixtures;
using RestRecipeAPI.TestFixtures.TestBuilder;
using RestRecipeApp.Db;
using Xunit;

namespace Tests.RestRecipeApp.E2E.Tests;

public class RecipeControllerTests : IClassFixture<RestRecipeAppWebApplicationFactory>
{
    private readonly RestRecipeAppWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;
    private readonly RecipesContext _context;

    public RecipeControllerTests(RestRecipeAppWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<RecipesContext>();
    }

    [Fact]
    public async Task Get_Recipes()
    {
        // var recipeSteps = new RecipeStepTestBuilder().Generate(5);
        // var products = new ProductTestBuilder().Generate(5);
        //
        //
        // var savedRecipeSteps = new List<RecipeStep>();
        //
        // foreach (var recipeStep in recipeSteps)
        // {
        //     var newRecipeStep = await _context.RecipeSteps.AddAsync(recipeStep);
        //     savedRecipeSteps.Add(new RecipeStep()
        //     {
        //         RecipeStepId = newRecipeStep.Entity.RecipeStepId,
        //         Description = newRecipeStep.Entity.Description,
        //         StepNumber = newRecipeStep.Entity.StepNumber
        //     });
        // }
        // await _context.SaveChangesAsync();
        //
        //
        // var savedProducts = new List<Product>();
        //
        // foreach (var product in products)
        // {
        //     var newProduct = await _context.Products.AddAsync(product);
        //     savedProducts.Add(new Product()
        //     {
        //         ProductId = newProduct.Entity.ProductId,
        //         Name = newProduct.Entity.Name,
        //     });
        // }
        // await _context.SaveChangesAsync();
        //
        //
        // var savedIngredients = new List<Ingredient>();
        // foreach (var savedProduct in savedProducts)
        // {
        //     var newIngredient = new IngredientTestBuilder(savedProduct.ProductId).Generate();
        //     var savedIngredient = await _context.Ingredients.AddAsync(newIngredient);
        //     savedIngredients.Add(new Ingredient()
        //     {
        //         IngredientId = savedIngredient.Entity.IngredientId,
        //         ProductId = savedIngredient.Entity.ProductId,
        //         Amount = savedIngredient.Entity.Amount,
        //         UnitOfMeasurement = savedIngredient.Entity.UnitOfMeasurement,
        //     });
        // }
        // await _context.SaveChangesAsync();

        var savedRecipe = await _context.Recipes.AddAsync(new Recipe()
        {
            CookingTime = 10,
            Name = "Test test",
            TotalPersons = 4,
        });
        
        var savedIngredient = await _context.Ingredients.AddAsync(new Ingredient()
        {
            Amount = 10,
            UnitOfMeasurement = UnitOfMeasurement.cl,
            ProductId = 11,
            
        });



        await _context.SaveChangesAsync();
        var response = await _httpClient.GetAsync("api/Recipe");
        response.EnsureSuccessStatusCode();
        var contentOrError = await ResponseObjectHelper.GetResponseObject<List<Recipe>>(response);
        contentOrError
            .Some((content) => Assert.Equal(new List<Recipe>(), content))
            .None(() => Console.Write("Something bad happened"));
    }
}