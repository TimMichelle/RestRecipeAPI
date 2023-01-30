using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipesApp.Domain;

namespace RestRecipeApp.Core.RequestDto;

public record CreateIngredientDto(
    CreateProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement,
    int? RecipeId = null
    );