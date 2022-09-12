using RecipesApp.Domain;

namespace RestRecipeApp.Controllers.Requests;

public record CreateIngredientDto(
    int ProductId,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement);