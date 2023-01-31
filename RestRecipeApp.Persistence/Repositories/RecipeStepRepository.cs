using LanguageExt;
using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class RecipeStepRepository : IRecipeStepRepository
{
    private readonly RecipesDbContext _recipesContext;

    public RecipeStepRepository(RecipesDbContext recipesContext)
    {
        _recipesContext = recipesContext;
    }
    public async Task<Either<DbError, RecipeStep>> GetRecipeStepById(int id)
    {
        try
        {
            return await _recipesContext.RecipeSteps
                .Where(foundRecipeStep => foundRecipeStep.RecipeStepId == id)
                .FirstAsync();
        }
        catch (Exception error)
        {
            return new DbError($"Could not retrieve recipe step: ${id} - error: {error.Message}");
        }
    }

    public async Task<Either<DbError, List<RecipeStep>>> GetRecipeSteps()
    {
        return await _recipesContext.RecipeSteps.ToListAsync();
    }

    public async Task<bool> RemoveRecipeStep(int id)
    {
        var recipeStep = await _recipesContext.RecipeSteps.FindAsync(id);
        if (recipeStep == null)
        {
            return false;
        }

        _recipesContext.RecipeSteps.Remove(recipeStep);
        await _recipesContext.SaveChangesAsync();
        return true;
    }

    public async Task<Either<DbError, RecipeStep>> CreateRecipeStep(CreateRecipeStepDto recipeStep)
    {
        try
        {
            var savedRecipeStep = await _recipesContext.RecipeSteps.AddAsync(recipeStep.MapRecipeStep());
            await _recipesContext.SaveChangesAsync();
            return savedRecipeStep.Entity;
        }
        catch (Exception e)
        {
            return new DbError($"Could not create recipe step: {e.Message}");
        }    }

    public async Task<Either<DbError, RecipeStep>> UpdateRecipeStep(UpdatedRecipeStepDto updatedRecipeStepDto)
    {
        var currentRecipeStep = await _recipesContext.RecipeSteps
            .FirstOrDefaultAsync(foundRecipeStep => foundRecipeStep.RecipeStepId == updatedRecipeStepDto.Id);
        if (currentRecipeStep == null)
        {
            return new DbError($"Could not find recipe step with id: {updatedRecipeStepDto.Id}");
        }

        currentRecipeStep.StepNumber = updatedRecipeStepDto.StepNumber ?? currentRecipeStep.StepNumber;
        currentRecipeStep.Description = updatedRecipeStepDto.Description ?? currentRecipeStep.Description;

        try
        {
            _recipesContext.Entry(currentRecipeStep).State = EntityState.Modified;
            await _recipesContext.SaveChangesAsync();
        }
        catch (Exception error)
        {
            return new DbError($"$Could not save changes to ingredient: {updatedRecipeStepDto.Id} - {error.Message}");
        }

        return currentRecipeStep;
    }
}