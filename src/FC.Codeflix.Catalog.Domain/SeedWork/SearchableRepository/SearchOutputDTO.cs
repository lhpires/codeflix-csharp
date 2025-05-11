using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

public class SearchOutputDTO<TAggregate>
    where TAggregate : AggregateRoot
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TAggregate> Items { get; set; }

    public SearchOutputDTO(
        int currentPage, 
        int perPage, 
        int total, 
        IReadOnlyList<TAggregate> items
    )
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }

}
