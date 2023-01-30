using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestRecipeApp.Core.Domain;

namespace RestRecipeApp.Core.RequestDto;

public record CreateIngredientDto(
    CreateProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement,
    int? RecipeId = null
    );