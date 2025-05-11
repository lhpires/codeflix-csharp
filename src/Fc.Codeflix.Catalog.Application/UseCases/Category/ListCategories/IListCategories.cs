
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public interface IListCategories : IRequestHandler<ListCategoriesInputDTO,ListCategoriesOutputDTO>
{
    public Task<ListCategoriesOutputDTO> Handle(ListCategoriesInputDTO input, CancellationToken cancellationToken);
}
