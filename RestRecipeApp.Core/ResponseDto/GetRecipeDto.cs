namespace RestRecipeApp.Core.ResponseDto;

public record GetRecipeDto(
    int Id,
    string Name,
    int CookingTime,
    int TotalPersons,
    List<GetIngredientDto> Ingredients,
    List<GetRecipeStepDto> Steps,
    GetImageDto? image);