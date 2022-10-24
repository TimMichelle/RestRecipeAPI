namespace RestRecipeApp.Core.RequestDto.Recipe;

public record UpdatedRecipeDto(int RecipeId, string? Name = null, int? CookingTime = null, int? TotalPersons = null);