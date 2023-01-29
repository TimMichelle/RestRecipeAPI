using RecipesApp.Domain;

namespace RestRecipeApp.Core.ResponseDto;

public record GetIngredientDto
(
    GetProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement
);