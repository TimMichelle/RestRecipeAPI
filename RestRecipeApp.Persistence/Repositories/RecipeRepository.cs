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
    public async Task<Either<DbError, Recipe>> GetRecipeById(int id)
    {
        try
        {
            return await _recipesContext.Recipes.Include(r => r.Steps)
                .Include(r => r.Image)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .Where(foundRecipe => foundRecipe.RecipeId == id).FirstAsync();
        }
        catch (DbException exception)
        {
            return new DbError($"Could not retrieve recipe with id: {id}");
        }
    }

    public async Task<Either<DbError, List<Recipe>>> GetRecipes()
    {
        // Todo add pagination
        try
        {
            return await _recipesContext.Recipes.
                Include(r => r.Steps)
                .Include(r => r.Image)
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
            .ThenInclude(i => i.Product)
            .Include(r => r.Steps)
            .FirstOrDefaultAsync(foundRecipe => foundRecipe.RecipeId == updatedRecipe.Id);

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

    public async Task<Image?> CreateImageForRecipe(int recipeId, CreateRecipeImageDto createRecipeImageDto)
    {
        Image image;
        using var memoryStream = new MemoryStream();
        await createRecipeImageDto.File.CopyToAsync(memoryStream);

        // Upload the file if less than 2 MB
        if (memoryStream.Length < 2097152)
        {
            var file = new Image()
            {
                Name = createRecipeImageDto.Name,
                Content = memoryStream.ToArray()
            };

            image = file;
             await _recipesContext.Images.AddAsync(file);
            await _recipesContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Files too large");
        }

        return image;
    }

    public async Task<Image?> GetImageOfRecipe(int id)
    {
        var recipeSome = _recipesContext.Recipes.Find(recipe => recipe.RecipeId == id);
        return recipeSome.Some<Image?>((recipe) =>
        {
            var image = _recipesContext.Images.Find(image => image.ImageId == recipe.Image.ImageId);
            return image.Some((x) => { return x; }).None(() => (Image)null);
        }).None(() => null);
    }
}