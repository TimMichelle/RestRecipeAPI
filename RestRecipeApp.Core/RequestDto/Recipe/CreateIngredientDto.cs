using RecipesApp.Domain;

namespace RestRecipeApp.Core.RequestDto.Recipe;

public record CreateIngredientDto(
    CreateProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement);