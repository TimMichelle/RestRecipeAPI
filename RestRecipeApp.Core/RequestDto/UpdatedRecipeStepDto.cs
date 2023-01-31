namespace RestRecipeApp.Core.RequestDto;

public record UpdatedRecipeStepDto(
    int Id, 
    int? StepNumber = null,
    string? Description = null);