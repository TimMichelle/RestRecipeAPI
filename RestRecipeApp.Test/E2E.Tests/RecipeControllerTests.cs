using Newtonsoft.Json;
using RecipesApp.Domain;
using Xunit;

namespace Tests.RestRecipeApp.E2E.Tests;

public class RecipeControllerTests: IClassFixture<RestRecipeAppWebApplicationFactory>
{
    private readonly RestRecipeAppWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public RecipeControllerTests(RestRecipeAppWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_Recipes()
    {
        var response = await _httpClient.GetAsync("api/Recipe");
        
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<Recipe>>(contentString);
        Assert.Empty(content);
    }
}