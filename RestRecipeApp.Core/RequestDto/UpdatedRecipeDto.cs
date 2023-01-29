namespace RestRecipeApp.Core.RequestDto;

public record UpdatedRecipeDto(int Id, string? Name = null, int? CookingTime = null, int? TotalPersons = null);