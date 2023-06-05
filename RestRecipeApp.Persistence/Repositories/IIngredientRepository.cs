using LanguageExt;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IIngredientRepository
{
    public Task<Either<DbError, Ingredient>> GetIngredientById(int id);
    public Task<IEnumerable<Ingredient>> GetIngredients(int? recipeId);
    public Task<bool> RemoveIngredient(int id);
    public Task<Either<DbError, Ingredient>> CreateIngredient(CreateIngredientDto ingredient);
    public Task<Either<DbError, Ingredient>> UpdateIngredient(UpdatedIngredientDto updatedIngredientDto);
}

