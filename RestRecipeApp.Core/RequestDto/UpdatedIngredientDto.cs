using RestRecipeApp.Core.Domain;

namespace RestRecipeApp.Core.RequestDto;

public record UpdatedIngredientDto(int Id, float? Amount = null, UnitOfMeasurement? UnitOfMeasurement = null);