namespace RestRecipeApp.Core.ResponseDto;

public record GetRecipeDto(string Name,
    int CookingTime,
    int TotalPersons,
    List<GetIngredientDto> Ingredients,
    List<GetRecipeStepDto> Steps);