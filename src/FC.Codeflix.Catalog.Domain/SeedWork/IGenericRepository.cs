
namespace FC.Codeflix.Catalog.Domain.SeedWork;

public interface IGenericRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot // O GENERICS TERÁ QUE IMPLEMENTAR O AggregateRoot
{
    // TASK É TIPO A PROMISSE DO JS, E SEMPRE QUANDO TEMOS UM MÉTODO ASSINCRONO A SER IMPLEMENTADO, VAMOS PASSAR O CANCELATTION TOKEN
    public Task<TAggregate> Insert(TAggregate aggregate,CancellationToken cancellationToken);

    public Task<TAggregate> Update(TAggregate aggregate,CancellationToken cancellationToken);

    public Task<TAggregate> Delete(TAggregate aggregate,CancellationToken cancellationToken);

    public Task<TAggregate> Get(Guid id, CancellationToken cancellationToken);

}
