using LanguageExt;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IRecipeRepository
{
    public Task<Recipe?> GetRecipeById(int id);
    public Task<Either<DbError, List<Recipe>>> GetRecipes();
    public Task<bool> RemoveRecipe(int id);
    public Task<Either<DbError, Recipe>> CreateRecipe(CreateRecipeDto recipe);
    public Task<Either<DbError, Recipe>> UpdateRecipe(UpdatedRecipeDto updatedRecipeDto);
}