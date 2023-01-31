namespace RestRecipeApp.Core.RequestDto;

public record CreateRecipeStepDto(
    int StepNumber,
    string Description,
    int? RecipeId = null);
    