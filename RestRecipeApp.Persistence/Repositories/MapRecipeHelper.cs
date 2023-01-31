using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Core.ResponseDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Persistence.Repositories;

public static class MapRecipeHelper
{
    public static GetRecipeDto MapGetRecipeDto(this Recipe recipe)
    {
        return new GetRecipeDto(
            recipe.RecipeId,
            recipe.Name,
            recipe.CookingTime,
            recipe.TotalPersons,
            recipe.Ingredients.Map(ingredient =>
                ingredient.MapGetIngredientDto()).ToList(),
            recipe.Steps.Map(step => step.MapGetRecipeStepDto()).ToList());
    }

   

    public static GetIngredientDto MapGetIngredientDto(this Ingredient ingredient)
    {
        return new GetIngredientDto(
            ingredient.IngredientId,
            ingredient.Product.MapGetProductDto(),
            ingredient.Amount,
            ingredient.UnitOfMeasurement);
    }

    public static GetProductDto MapGetProductDto(this Product product)
    {
        return new GetProductDto(product.ProductId, product.Name);
    }

    public static GetRecipeStepDto MapGetRecipeStepDto(this RecipeStep step)
    {
        return new GetRecipeStepDto(step.RecipeStepId, step.StepNumber, step.Description);
    }
    
    public static Ingredient MapIngredient(this CreateIngredientDto ingredient)
    {
        var newIngredient =  new Ingredient()
        {
            
            UnitOfMeasurement = ingredient.UnitOfMeasurement,
            Amount = ingredient.Amount,
            Product = ingredient.Product.MapProduct()
        };
        if (ingredient.RecipeId != null)
        {
            newIngredient.RecipeId = (int)ingredient.RecipeId!;
        }

        return newIngredient;
    }

    public static Product MapProduct(this CreateProductDto product)
    {
        return new Product()
        {
            Name = product.Name
        };
    }
    
    public static RecipeStep MapRecipeStep(this CreateRecipeStepDto recipeStep)
    {
        var newRecipeStep =  new RecipeStep()
        {
            StepNumber = recipeStep.StepNumber,
            Description = recipeStep.Description
        };
        if (recipeStep.RecipeId != null)
        {
            newRecipeStep.RecipeId = (int)recipeStep.RecipeId;
        }

        return newRecipeStep;
    }
}