
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

public class DeleteCategoryInputDTO : IRequest
{
    public Guid Id { get; set; }

    public DeleteCategoryInputDTO(
        Guid id
    )
    {
        Id = id;
    }
}
