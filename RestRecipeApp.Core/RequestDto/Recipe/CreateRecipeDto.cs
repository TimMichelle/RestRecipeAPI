namespace RestRecipeApp.Core.RequestDto.Recipe;

public record CreateRecipeDto(
    string Name,
    int CookingTime,
    int TotalPersons,
    List<CreateIngredientDto> Ingredients,
    List<CreateRecipeStepDto> Steps
    );