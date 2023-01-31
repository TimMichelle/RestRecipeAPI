using FluentValidation;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeApp.Validation;

public class CreateRecipeStepDtoValidator: AbstractValidator<CreateRecipeStepDto>
{
    public CreateRecipeStepDtoValidator()
    {
        RuleFor(x => x.RecipeId).NotNull();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.StepNumber).NotNull();
    }
}