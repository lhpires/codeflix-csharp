
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategoryInputDTO : IRequest<CategoryOutputDTO>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }


    public UpdateCategoryInputDTO(Guid id,string name, string description, bool isActive)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
    }
}
