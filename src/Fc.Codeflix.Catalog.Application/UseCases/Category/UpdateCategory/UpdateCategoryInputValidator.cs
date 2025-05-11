
using FluentValidation;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategoryInputValidator : AbstractValidator<UpdateCategoryInputDTO>
{
    public UpdateCategoryInputValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();

    }
}
