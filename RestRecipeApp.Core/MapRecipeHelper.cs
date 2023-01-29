using RestRecipeApp.Core.ResponseDto;
using RestRecipeApp.Persistence.Models;

namespace RestRecipeApp.Core;

public static class MapRecipeHelper
{
    public static GetRecipeDto MapGetRecipeDto(this Recipe recipe)
    {
        return new GetRecipeDto(
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
            ingredient.MapGetProductDto(),
            ingredient.Amount,
            ingredient.UnitOfMeasurement);
    }

    private static GetProductDto MapGetProductDto(this Ingredient ingredient)
    {
        return new GetProductDto(ingredient.Product.Name);
    }

    public static GetRecipeStepDto MapGetRecipeStepDto(this RecipeStep step)
    {
        return new GetRecipeStepDto(step.StepNumber, step.Description);
    }
}