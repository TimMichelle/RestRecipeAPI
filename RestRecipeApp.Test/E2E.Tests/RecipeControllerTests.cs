using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestRecipeAPI.TestFixtures;
using RestRecipeAPI.TestFixtures.TestBuilder;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence;
using RestRecipeApp.Persistence.Models;
using Xunit;

namespace Tests.RestRecipeApp.E2E.Tests;

public class RecipeControllerTests : IClassFixture<RestRecipeAppWebApplicationFactory>, IDisposable
{
    private readonly RestRecipeAppWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;
    private readonly RecipesDbContext _context;

    public RecipeControllerTests(RestRecipeAppWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<RecipesDbContext>();
    }

    [Fact]
    public async Task Get_Recipes()
    {
        // Create Recipe
        var savedRecipe = await _context.Recipes.AddAsync(new Recipe()
        {
            CookingTime = 10,
            Name = "Test test",
            TotalPersons = 4,
        });
        await _context.SaveChangesAsync();
        var recipeId = savedRecipe.Entity.RecipeId;
        
        // Create RecipeSteps
        var savedRecipeSteps = new List<RecipeStep>();
        
        for (var i = 0; i <= 3; i++)
        {
            var recipeStep = new RecipeStepTestBuilder(recipeId).Generate();
        
            var newRecipeStep = await _context.RecipeSteps.AddAsync(recipeStep);
            await _context.SaveChangesAsync();

            savedRecipeSteps.Add(new RecipeStep()
            {
                RecipeStepId = newRecipeStep.Entity.RecipeStepId,
                Description = newRecipeStep.Entity.Description,
                StepNumber = newRecipeStep.Entity.StepNumber,
                RecipeId = savedRecipe.Entity.RecipeId,
            });
        }
        await _context.SaveChangesAsync();
        
        // Create Products
        var savedProducts = new List<Product>();
        
        for (var i = 0; i <= 5; i++)
        {
            var product =  new ProductTestBuilder().Generate();
            var newProduct = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            savedProducts.Add(new Product()
            {
                ProductId = newProduct.Entity.ProductId,
                Name = newProduct.Entity.Name,
            });
        }
        
        // Create Ingredients
        var savedIngredients = new List<Ingredient>();
        foreach (var savedProduct in savedProducts)
        {
            var newIngredient = new IngredientTestBuilder(savedProduct.ProductId, recipeId).Generate();
            var savedIngredient = await _context.Ingredients.AddAsync(newIngredient);
            await _context.SaveChangesAsync();

            savedIngredients.Add(new Ingredient()
            {
                Amount = savedIngredient.Entity.Amount,
                UnitOfMeasurement = savedIngredient.Entity.UnitOfMeasurement,
                ProductId = savedProduct.ProductId,
                RecipeId = recipeId
            });
        }

        var response = await _httpClient.GetAsync("api/Recipe");
        response.EnsureSuccessStatusCode();
        var contentOrError = await ResponseObjectHelper.GetResponseObject<List<Recipe>>(response);
        contentOrError
            .Some((content) =>
            {
                Assert.Equal(content[0].Name, savedRecipe.Entity.Name);
                Assert.Equal(6, content[0].Ingredients.Count);
                Assert.Equal(4, content[0].Steps.Count);
            })
            .None(() => Console.Write("Something bad happened"));
    }

    [Fact]
    public async Task Create_Recipe()
    {
        var recipeDto = new CreateRecipeDtoTestBuilder().Generate();
        var stringifiedContent = JsonConvert.SerializeObject(recipeDto);
        var requestObject = new StringContent(stringifiedContent,  Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Recipe", requestObject);
        response.EnsureSuccessStatusCode();

        var contentOrError = await ResponseObjectHelper.GetResponseObject<Recipe>(response);
        contentOrError.Some(content =>
        {
            Assert.Equal(content.Name, recipeDto.Name);
            Assert.Equal(content.Ingredients.Count, recipeDto.Ingredients.Count);
            Assert.Equal(content.Steps.Count, recipeDto.Steps.Count);
        }).None(() => Console.Write("Something bad happened"));

    }
    
    [Fact]
    public async Task Create_recipe_without_ingredients()
    {
        var ingredientsList = new List<CreateIngredientDto>();
        var recipeDto = new CreateRecipeDtoTestBuilder(ingredientsList).Generate();
        var stringifiedContent = JsonConvert.SerializeObject(recipeDto);
        var requestObject = new StringContent(stringifiedContent,  Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Recipe", requestObject);
        response.EnsureSuccessStatusCode();

        var contentOrError = await ResponseObjectHelper.GetResponseObject<Recipe>(response);
        contentOrError.Some(content =>
        {
            Assert.Equal(content.Name, recipeDto.Name);
            Assert.Empty(content.Ingredients);
            Assert.Equal(content.Steps.Count, recipeDto.Steps.Count);
        }).None(() => Console.Write("Something bad happened"));
    }

    [Theory]
    [InlineData(null, null, 10)]
    [InlineData(null, null, null)]
    [InlineData("Updated Recipe", null, null)]
    [InlineData("Updated Recipe", 45, null)]
    public async Task Update_Recipe(string? name, int? cookingTime, int? totalPersons)
    {
        var alreadyCreatedRecipe = new RecipeTestBuilder().Generate();
        var savedRecipeEntity = await _context.Recipes.AddAsync(alreadyCreatedRecipe);
        await _context.SaveChangesAsync();
        var savedRecipe =  savedRecipeEntity.Entity;
        var updatedRecipeDto = new UpdatedRecipeDto(savedRecipe.RecipeId, name, cookingTime, totalPersons);
       
        var stringifiedContent = JsonConvert.SerializeObject(updatedRecipeDto);
        var requestObject = new StringContent(stringifiedContent,  Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync("api/Recipe", requestObject);
        response.EnsureSuccessStatusCode();
        
        var contentOrError = await ResponseObjectHelper.GetResponseObject<Recipe>(response);
        contentOrError.Some(content =>
        {
            Assert.Equal(content.Name, !string.IsNullOrEmpty(name) ? name : alreadyCreatedRecipe.Name);
            Assert.Equal(content.CookingTime, cookingTime ?? alreadyCreatedRecipe.CookingTime);
            Assert.Equal(content.TotalPersons, totalPersons ?? alreadyCreatedRecipe.TotalPersons);
        }).None(() => Console.Write("Something bad happened"));
    }

    public void Dispose()
    {
        _context.Database.ExecuteSqlRaw("truncate table \"Ingredients\" cascade; truncate table \"Products\" cascade;truncate table \"RecipeSteps\" cascade;truncate table \"Recipes\" cascade;");
        _factory.Dispose();
        _httpClient.Dispose();
        _context.Dispose();
    }
}