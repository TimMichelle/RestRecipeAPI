namespace RestRecipeApp.Core.RequestDto;

public record CreateRecipeDto(
    string Name,
    int CookingTime,
    int TotalPersons,
    List<CreateIngredientDto> Ingredients,
    List<CreateRecipeStepDto> Steps
    );