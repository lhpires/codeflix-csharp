
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Fc.Codeflix.Catalog.Application.Common;

public abstract class PaginateListOutput<TOutputDTO>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutputDTO> Items { get; set; }

    protected PaginateListOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<TOutputDTO> 
        items
    )
    {
        Page = page;
        PerPage = perPage;
        Total = total;
        Items = items;
    }

}
