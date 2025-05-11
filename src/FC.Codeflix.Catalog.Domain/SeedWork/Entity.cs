namespace FC.Codeflix.Catalog.Domain.SeedWork;

public abstract class Entity
{
    // TEM QUE SER PROTECTED, PORQUE AS CLASSES QUE HERDAM A ENTITY PODEM ALTERAR ESSA PROPRIEDADE
    public Guid Id { get; protected set; }

    public Entity() => Id = Guid.NewGuid();
}
