using System.Data.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public class RecipeRepository: IRecipeRepository
{
    private readonly RecipesDbContext _recipesContext;
    public RecipeRepository(RecipesDbContext recipesContext)
    {
        _recipesContext = recipesContext;
    }
    public async Task<Recipe?> GetRecipeById(int id)
    {
        return await _recipesContext.Recipes.FindAsync(id);
    }

    public async Task<Either<DbError, List<Recipe>>> GetRecipes()
    {
        // Todo add pagination
        try
        {
            return await _recipesContext.Recipes.
                Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .ToListAsync();
        }
        catch (DbException exception)
        {
            return new DbError("Could not retrieve recipes");
        }
    }

    public async Task<bool> RemoveRecipe(int id)
    {
        var recipe = await _recipesContext.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return false;
        }
        
        _recipesContext.Recipes.Remove(recipe);
        await _recipesContext.SaveChangesAsync();
        return true;
    }

    public async Task<Either<DbError, Recipe>> CreateRecipe(CreateRecipeDto recipe)
    {
        var newlyCreatedRecipe = new Recipe()
        {
            Name = recipe.Name,
            CookingTime = recipe.CookingTime,
            TotalPersons = recipe.TotalPersons,
            Ingredients = recipe.Ingredients.Select((ingredient) => ingredient.MapIngredient()).ToList(),
            Steps = recipe.Steps.Select((step) => new RecipeStep()
            {
                Description = step.Description,
                StepNumber = step.StepNumber
            }).ToList()
        };

        try
        {
            var savedRecipe = await _recipesContext.Recipes.AddAsync(newlyCreatedRecipe);
            await _recipesContext.SaveChangesAsync();
            return savedRecipe.Entity;
        }
        catch (DbUpdateException e)
        {
            return new DbError($"Could not create recipeDto: {e.Message}");
        }
    }

   

    public async Task<Either<DbError, Recipe>> UpdateRecipe(UpdatedRecipeDto updatedRecipe)
    {
        var currentRecipe = await _recipesContext.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Steps).FirstOrDefaultAsync(foundRecipe => foundRecipe.RecipeId == updatedRecipe.Id);

        if (currentRecipe == null)
        {
            return new DbError($"Could not find recipe with id: {updatedRecipe.Id}");
        }

        currentRecipe.Name = !string.IsNullOrEmpty(updatedRecipe.Name) ? updatedRecipe.Name : currentRecipe.Name;
        currentRecipe.CookingTime = updatedRecipe.CookingTime ?? currentRecipe.CookingTime;
        currentRecipe.TotalPersons = updatedRecipe.TotalPersons ?? currentRecipe.TotalPersons;
        
        _recipesContext.Entry(currentRecipe).State = EntityState.Modified;
            
        try
        {
            await _recipesContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException error)
        {
            return new DbError($"Could not save updates to recipe: {updatedRecipe.Id}");
        }

        return currentRecipe;
    }
}