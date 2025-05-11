
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public interface IUpdateCategory : IRequestHandler<UpdateCategoryInputDTO, CategoryOutputDTO>
{
    public Task<CategoryOutputDTO> Handle(UpdateCategoryInputDTO input,CancellationToken cancellationToken);
}
