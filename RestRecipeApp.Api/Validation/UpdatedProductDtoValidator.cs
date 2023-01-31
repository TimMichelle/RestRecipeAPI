using FluentValidation;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeApp.Validation;

public class UpdatedProductDtoValidator: AbstractValidator<UpdatedProductDto>
{
    public UpdatedProductDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty();
    }
}