using LanguageExt;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public interface IRecipeStepRepository
{
    public Task<Either<DbError, RecipeStep>> GetRecipeStepById(int id);
    public Task<Either<DbError, List<RecipeStep>>> GetRecipeSteps();
    public Task<bool> RemoveRecipeStep(int id);
    public Task<Either<DbError, RecipeStep>> CreateRecipeStep(CreateRecipeStepDto recipeStep);
    public Task<Either<DbError, RecipeStep>> UpdateRecipeStep(UpdatedRecipeStepDto updatedRecipeStepDto);
}