
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public interface ICreateCategory : IRequestHandler<CreateCategoryInputDTO, CategoryOutputDTO>
{
    public Task<CategoryOutputDTO> Handle(CreateCategoryInputDTO input,CancellationToken cancellationToken);
}
