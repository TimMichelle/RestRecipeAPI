using Microsoft.EntityFrameworkCore;
using RecipesApp.Domain;
using RestRecipeApp.Controllers.Requests;
using RestRecipeApp.Db;

namespace RestRecipeApp.Repositories;

public class RecipeRepository: IRecipeRepository
{
    private readonly RecipesContext _recipesContext;
    public RecipeRepository(RecipesContext recipesContext)
    {
        _recipesContext = recipesContext;
    }
    public async Task<Recipe?> GetRecipeById(int id)
    {
        return await _recipesContext.Recipes.FindAsync(id);
    }

    public async Task<List<Recipe>> GetRecipes()
    {
        return await _recipesContext.Recipes.ToListAsync();
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

    public async Task<Recipe> CreateRecipe(CreateRecipeDto recipe)
    {

        var newlyCreatedRecipe = new Recipe()
        {
            Name = recipe.Name,
            CookingTime = recipe.CookingTime,
            TotalPersons = recipe.TotalPersons,
            Ingredients = recipe.Ingredients.Select((ingredient) => new Ingredient()
            {
                UnitOfMeasurement = ingredient.UnitOfMeasurement,
                Amount = ingredient.Amount,
                ProductId = ingredient.ProductId
            }).ToList(),
            Steps = recipe.Steps.Select((step) => new RecipeStep()
            {
                Description = step.Description,
                StepNumber = step.StepNumber
            }).ToList()
        };
        var savedRecipe = await _recipesContext.Recipes.AddAsync(newlyCreatedRecipe);
        await _recipesContext.SaveChangesAsync();
        return savedRecipe.Entity;
    }

    public Task<Recipe> UpdateRecipe(int id)
    {
        throw new NotImplementedException();
    }
}