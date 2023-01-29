using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipesApp.Domain;

namespace RestRecipeApp.Core.RequestDto;

public record CreateIngredientDto(
    int RecipeId,
    CreateProductDto Product,
    float Amount,
    UnitOfMeasurement UnitOfMeasurement);