namespace RestRecipeApp.Core.RequestDto.Recipe;

public record CreateRecipeStepDto(
    int StepNumber,
    string Description);
    