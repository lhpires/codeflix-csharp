
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;


public class CreateCategoryInputDTO : IRequest<CategoryOutputDTO>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public CreateCategoryInputDTO(
        string name,
        string? description = null,
        bool isActive = true
    )
    {
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
    }
}
