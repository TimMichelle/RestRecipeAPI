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
            ingredient.MapGetProductDto(),
            ingredient.Amount,
            ingredient.UnitOfMeasurement);
    }

    private static GetProductDto MapGetProductDto(this Ingredient ingredient)
    {
        return new GetProductDto(ingredient.Product.ProductId, ingredient.Product.Name);
    }

    public static GetRecipeStepDto MapGetRecipeStepDto(this RecipeStep step)
    {
        return new GetRecipeStepDto(step.RecipeStepId, step.StepNumber, step.Description);
    }
    
    public static Ingredient MapIngredient(this CreateIngredientDto ingredient)
    {
        return new Ingredient()
        {
            RecipeId = ingredient.RecipeId,
            UnitOfMeasurement = ingredient.UnitOfMeasurement,
            Amount = ingredient.Amount,
            Product = ingredient.MapProduct()
        };
    }

    private static Product MapProduct(this CreateIngredientDto ingredient)
    {
        return new Product()
        {
            Name = ingredient.Product.Name
        };
    }
}