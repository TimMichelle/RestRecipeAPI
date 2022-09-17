using RecipesApp.Domain;

namespace RestRecipeApp.Core.RequestDto.Recipe;

public record CreateIngredientDto(
    int ProductId,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement);