
using Fc.Codeflix.Catalog.Application.Common;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInputDTO : PaginateListInput,IRequest<ListCategoriesOutputDTO>
{
    public ListCategoriesInputDTO(
        int page, 
        int perPage, 
        string search,
        string sort, 
        SearchOrder dir
    ) : base(page, perPage, search, sort, dir)
    {
    }
}
