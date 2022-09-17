using LanguageExt;
using RecipesApp.Domain;
using RestRecipeApp.Core.RequestDto.Recipe;

namespace RestRecipeApp.Repositories;

public interface IRecipeRepository
{
    public Task<Recipe?> GetRecipeById(int id);
    public Task<Either<DbError, List<Recipe>>> GetRecipes();
    public Task<bool> RemoveRecipe(int id);
    public Task<Recipe> CreateRecipe(CreateRecipeDto recipe);
    public Task<Recipe> UpdateRecipe(int id);
}