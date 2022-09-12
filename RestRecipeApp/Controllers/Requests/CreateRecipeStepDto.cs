namespace RestRecipeApp.Controllers.Requests;

public record CreateRecipeStepDto(
    int StepNumber,
    string Description);
    