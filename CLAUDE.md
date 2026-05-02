# FC.Codeflix.Catalog

Catalog service for a streaming platform built with .NET 8, Clean Architecture, and DDD.

## Architecture

```
src/
  FC.Codeflix.Catalog.Domain/        → Entities, exceptions, repository interfaces, validations
  Fc.Codeflix.Catalog.Application/   → Use cases (MediatR), DTOs, UoW interface
tests/
  FC.Codeflix.Catalog.UnitTests/     → Unit tests (xUnit + Moq + FluentAssertions + Bogus)
```

### Principles
- **Clean Architecture**: Domain has no dependencies; Application depends only on Domain
- **DDD**: Entities are aggregate roots; validation lives inside the domain
- **CQRS via MediatR**: Commands (Create, Update, Delete) and Queries (Get, List) as `IRequest<T>`
- **Repository Pattern**: Interface defined in Domain, implementations in infrastructure

## Domain Layer

### SeedWork
- `Entity` — base class with `Id` (Guid, set in constructor)
- `AggregateRoot : Entity` — aggregate root marker
- `IGenericRepository<TAggregate>` — `Insert`, `Update`, `Delete`, `Get` (all async + CancellationToken)
- `ISearchableReposotory<TAggregate>` — `Search(SearchInputDTO, CancellationToken)`
- `SearchInputDTO` — `Page`, `PerPage`, `Search`, `OrderBy`, `Order`
- `SearchOutputDTO<T>` — `CurrentPage`, `PerPage`, `Total`, `Items`
- `SearchOrder` — enum: `Asc = 1`

### Validation
`DomainValidation` (static) — throws `EntityValidationException` with standard messages:
- `NotNull` → `"{field} should not be null"`
- `NotNullOrEmpty` → `"{field} should not be null or empty"`
- `MinLength` → `"{field} cannot be less than {min} characters long"`
- `MaxLength` → `"{field} cannot have more than {max} characters long"`

### Entities
| Entity | Properties | Rules |
|--------|-----------|-------|
| `Category` | Name, Description, IsActive, CreatedAt | Name: 3–255 chars; Description: not null, max 10000 |

### Repository Interfaces
- `ICategoryRepository : IGenericRepository<Category>, ISearchableReposotory<Category>`

## Application Layer

### Interfaces
- `IUnitOfWork` — `Commit(CancellationToken)`

### Common DTOs
- `PaginateListInput` — `Page`, `PerPage`, `Search`, `Sort`, `Dir`
- `PaginateListOutput<T>` — `Page`, `PerPage`, `Total`, `Items`

### Use Cases — Category (reference pattern for new aggregates)
Each use case has: interface `I*`, input DTO, output DTO, and `*UseCase : I*` class.

| Use Case | Input | Output |
|----------|-------|--------|
| `CreateCategory` | Name, Description, IsActive | `CategoryOutputDTO` |
| `GetCategory` | Id | `CategoryOutputDTO` |
| `UpdateCategory` | Id, Name, Description, IsActive | `CategoryOutputDTO` |
| `DeleteCategory` | Id | `Unit` (MediatR) |
| `ListCategories` | `PaginateListInput` + SearchOrder | `ListCategoriesOutputDTO` |

`CategoryOutputDTO` has a `FromCategory(Category)` factory method.

## Unit Tests

### Stack
- **xUnit** — test framework
- **Moq** — mock `ICategoryRepository` and `IUnitOfWork`
- **FluentAssertions** — fluent assertions (`x.Should().Be(...)`)
- **Bogus** — data generation with `Faker` (locale `pt_BR`)

### Fixture Pattern
```csharp
[CollectionDefinition(nameof(XxxTestFixture))]
public class XxxTestFixtureCollection : ICollectionFixture<XxxTestFixture> { }

public class XxxTestFixture : BaseFixture
{
    public Mock<IXxxRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();
    public DomainEntity.Xxx GetValidXxx() => new(...);
    public XxxInputDTO GetValidInputDTO(...) => new(...);
}
```

### Test Pattern
```csharp
[Collection(nameof(XxxTestFixture))]
public class XxxTest
{
    private readonly XxxTestFixture _fixture;
    public XxxTest(XxxTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(MethodName))]
    [Trait("Application", "XxxUseCase - UseCases")]
    public async void MethodName() { ... }
}
```

### TestDataGenerator Pattern
```csharp
public class XxxTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputsDtos(int times)
    {
        // switch (idx % totalCases) yielding invalid cases
    }
}
```

### Traits
- `[Trait("Domain", "Category - Aggregates")]`
- `[Trait("Application", "CreateCategory - UseCases")]`

## Naming Conventions

| Element | Example |
|---------|---------|
| Domain namespace | `FC.Codeflix.Catalog.Domain` |
| Application namespace | `Fc.Codeflix.Catalog.Application` (lowercase f) |
| Tests namespace | `FC.Codeflix.Catalog.UnitTests` |
| Use case class | `CreateCategoryUseCase` |
| Use case interface | `ICreateCategory` |
| Input DTO | `CreateCategoryInputDTO` |
| Output DTO | `CategoryOutputDTO` |
| Fixture | `CreateCategoryTestFixture` |
| Data generator | `CreateCategoryTestDataGenerator` |
| Repository interface | `ICategoryRepository` |

## Adding a New Aggregate

1. **Domain**: Create `Entity/AggregateName.cs` extending `AggregateRoot`, validations via `DomainValidation`
2. **Domain**: Create `Repository/IAggregateNameRepository.cs` extending `IGenericRepository<T>` and `ISearchableReposotory<T>`
3. **Application**: Create `UseCases/AggregateName/Common/AggregateNameOutputDTO.cs` with `FromX()` factory
4. **Application**: Create use cases Create, Get, Update, Delete, List (each with interface + input DTO + use case class)
5. **Tests**: Create fixtures, tests, and data generators mirroring the `Category` structure
