
using Fc.Codeflix.Catalog.Application.Common;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInputDTO : PaginateListInput,IRequest<ListCategoriesOutputDTO>
{
    public ListCategoriesInputDTO(
        int page = 1, 
        int perPage = 15, 
        string search = "",
        string sort = "", 
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir) {}
}
