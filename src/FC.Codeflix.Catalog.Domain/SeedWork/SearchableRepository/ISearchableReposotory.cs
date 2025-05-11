
namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

public interface ISearchableReposotory<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<SearchOutputDTO<TAggregate>> Search(SearchInputDTO input,CancellationToken cancellationToken);
}
