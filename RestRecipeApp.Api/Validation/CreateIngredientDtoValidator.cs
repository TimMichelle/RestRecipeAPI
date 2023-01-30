using FluentValidation;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeApp.Validation;

public class CreateIngredientDtoValidator : AbstractValidator<CreateIngredientDto>
{
    public CreateIngredientDtoValidator()
    {
        RuleFor(x => x.RecipeId).NotNull();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.UnitOfMeasurement).NotEmpty();
        RuleFor(x => x.Product.Name).NotEmpty();
    }
}