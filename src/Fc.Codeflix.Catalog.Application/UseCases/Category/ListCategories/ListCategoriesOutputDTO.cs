
using Fc.Codeflix.Catalog.Application.Common;
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesOutputDTO : PaginateListOutput<CategoryOutputDTO>
{
    public ListCategoriesOutputDTO(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<CategoryOutputDTO> items) : base(page, perPage, total, items)
    {
    }
}
