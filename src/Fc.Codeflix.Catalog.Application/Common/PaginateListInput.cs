
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Fc.Codeflix.Catalog.Application.Common;

public abstract class PaginateListInput
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string Search { get; set; }
    public string Sort { get; set; }
    public SearchOrder Dir { get; set; }

    protected PaginateListInput(
        int page, 
        int perPage,
        string search,
        string sort, 
        SearchOrder dir
    )
    {
        Page = page;
        PerPage = perPage;
        Search = search;
        Sort = sort;
        Dir = dir;
    }
}
