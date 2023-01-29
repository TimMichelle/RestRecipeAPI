namespace RestRecipeApp.Core.ResponseDto;

public record GetRecipeStepDto(
    int Id,
    int StepNumber,
    string Description);