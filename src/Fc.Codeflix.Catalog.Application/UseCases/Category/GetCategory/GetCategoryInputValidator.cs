
using FluentValidation;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInputValidator: AbstractValidator<GetCategoryInputDTO>
{
    public GetCategoryInputValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
