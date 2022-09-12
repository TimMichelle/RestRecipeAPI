using RecipesApp.Domain;
using RestRecipeApp.Controllers.Requests;

namespace RestRecipeApp.Repositories;

public interface IRecipeRepository
{
    public Task<Recipe?> GetRecipeById(int id);
    public Task<List<Recipe>> GetRecipes();
    public Task<bool> RemoveRecipe(int id);
    public Task<Recipe> CreateRecipe(CreateRecipeDto recipe);
    public Task<Recipe> UpdateRecipe(int id);
}