using RecipesApp.Domain;

namespace RestRecipeApp.Core.ResponseDto;

public record GetIngredientDto
(
    int Id,
    GetProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement
);