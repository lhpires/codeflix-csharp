
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInputDTO, CategoryOutputDTO>
{
    public Task<CategoryOutputDTO> Handle(GetCategoryInputDTO input,CancellationToken cancellationToken);
}
