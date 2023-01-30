using FluentValidation;
using RestRecipeApp.Core.RequestDto;

namespace RestRecipeApp.Validation;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
