
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInputDTO: IRequest<CategoryOutputDTO>
{
    public Guid Id { get; set; }

    public GetCategoryInputDTO(Guid id)
    {
        Id = id;
    }
}
