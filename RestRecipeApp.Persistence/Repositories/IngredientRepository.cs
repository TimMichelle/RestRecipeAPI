using LanguageExt;
using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly RecipesDbContext _recipesContext;

    public IngredientRepository(RecipesDbContext recipesContext)
    {
        _recipesContext = recipesContext;
    }


    public async Task<Either<DbError, Ingredient>> GetIngredientById(int id)
    {
        try
        {
            return await _recipesContext.Ingredients
                .Where(foundIngredient => foundIngredient.IngredientId == id)
                .Include(x => x.Product)
                .FirstAsync();
        }
        catch (Exception error)
        {
            return new DbError($"Could not retrieve ingredient: ${id} - error: {error.Message}");
        }
    }

    public async Task<IEnumerable<Ingredient>> GetIngredients(int? recipeId = null)
    {
        return await _recipesContext.Ingredients.Include(i => i.Product)
            .Where(x => recipeId == null || x.RecipeId == recipeId).ToListAsync();
    }

    public async Task<bool> RemoveIngredient(int id)
    {
        var ingredient = await _recipesContext.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return false;
        }

        _recipesContext.Ingredients.Remove(ingredient);
        await _recipesContext.SaveChangesAsync();
        return true;
    }

    public async Task<Either<DbError, Ingredient>> CreateIngredient(CreateIngredientDto ingredient)
    {
        try
        {
            var savedIngredient = await _recipesContext.Ingredients.AddAsync(ingredient.MapIngredient());
            await _recipesContext.SaveChangesAsync();
            return savedIngredient.Entity;
        }
        catch (Exception e)
        {
            return new DbError($"Could not create ingredient: {e.Message}");
        }
    }

    public async Task<Either<DbError, Ingredient>> UpdateIngredient(UpdatedIngredientDto updatedIngredientDto)
    {
        var currentIngredient = await _recipesContext.Ingredients
            .Include(i => i.Product)
            .FirstOrDefaultAsync(foundIngredient => foundIngredient.IngredientId == updatedIngredientDto.Id);
        if (currentIngredient == null)
        {
            return new DbError($"Could not find ingredient with id: {updatedIngredientDto.Id}");
        }

        currentIngredient.Amount = updatedIngredientDto.Amount ?? currentIngredient.Amount;
        currentIngredient.UnitOfMeasurement =
            updatedIngredientDto.UnitOfMeasurement ?? currentIngredient.UnitOfMeasurement;

        try
        {
            _recipesContext.Entry(currentIngredient).State = EntityState.Modified;
            await _recipesContext.SaveChangesAsync();
        }
        catch (Exception error)
        {
            return new DbError($"$Could not save changes to ingredient: {updatedIngredientDto.Id} - {error.Message}");
        }

        return currentIngredient;
    }
}