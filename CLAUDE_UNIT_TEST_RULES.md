# Test Rules

Stack: xUnit + Moq + FluentAssertions + Bogus (`Faker("pt_BR")`).

## Folder layout

```
tests/FC.Codeflix.Catalog.UnitTests/
  Common/BaseFixture.cs
  Domain/Entity/{Aggregate}/{Aggregate}Test.cs + {Aggregate}TestFixture.cs
  Application/Common/{Aggregate}UseCasesBaseFixture.cs
  Application/{Aggregate}/{UseCase}/
      {UseCase}Test.cs
      {UseCase}TestFixture.cs
      {UseCase}TestDataGenerator.cs   # only if using [Theory]+[MemberData]
```

## Fixture hierarchy

`BaseFixture` (Faker `pt_BR`) → `{Aggregate}UseCasesBaseFixture` (mocks + entity factories) → `{UseCase}TestFixture` (input DTO factory).

```csharp
// {UseCase}TestFixture.cs
[CollectionDefinition(nameof(XxxTestFixture))]
public class XxxTestFixtureCollection : ICollectionFixture<XxxTestFixture> { }

public class XxxTestFixture : CategoryUseCasesBaseFixture
{
    public XxxInputDTO GetInputDTO() => new(GetValidCategoryName(), GetValidDescription(), GetRandomBoolean());
}
```

`{Aggregate}UseCasesBaseFixture` exposes: `GetRepositoryMock()`, `GetUnitOfWork()`, `GetValidCategory()`, `GetValidCategoryName()` (≥3 chars), `GetValidDescription()` (≤10000 chars), `GetRandomBoolean()`.

Domain fixtures inherit `BaseFixture` directly and expose `InstantiateValidXxx()`.

## Test class template

```csharp
[Collection(nameof(XxxTestFixture))]
public class XxxTest
{
    private readonly XxxTestFixture _fixture;
    public XxxTest(XxxTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(MethodName))]
    [Trait("Application", "Xxx - UseCases")]
    public async void MethodName()
    {
        var repo = _fixture.GetRepositoryMock();
        var uow = _fixture.GetUnitOfWork();
        var input = _fixture.GetInputDTO();
        var useCase = new XxxUseCase(repo.Object, uow.Object);

        var execute = await useCase.Handle(input, CancellationToken.None);

        execute.Should().NotBeNull();
        execute.Id.Should().NotBeEmpty();
        execute.CreatedAt.Should().NotBeSameDateAs(default);
        repo.Verify(x => x.Insert(It.IsAny<DomainEntity.Category>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

## Required per test method

- `[Fact]` or `[Theory]` with `DisplayName = nameof(MethodName)`
- `[Trait(camada, "{Subject} - {Group}")]`

| Layer | Trait |
|-------|-------|
| Domain entity | `("Domain", "{Aggregate} - Aggregates")` |
| Domain validation | `("Domain", "DomainValidation - Validation")` |
| Application use case | `("Application", "{UseCase} - UseCases")` |

## Asserts & mocks

- FluentAssertions only (`Should()...`).
- Async exception: `Func<Task> task = async () => await useCase.Handle(input, CT.None); await task.Should().ThrowAsync<TException>().WithMessage(msg);`
- Mock setup: `ReturnsAsync` / `ThrowsAsync`; match args with `It.IsAny<T>()` or `It.Is<T>(predicate)`.
- Always `Verify(..., Times.Once)` repository and UoW interactions.

## TestDataGenerator (parameterized cases)

Static method returning `IEnumerable<object[]>`, called via `[MemberData(nameof(...), parameters: N, MemberType = typeof(...))]`. Use `switch (idx % totalCases)` to cycle invalid scenarios.

```csharp
public class XxxTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputsDtos(int times)
    {
        var fixture = new XxxTestFixture();
        for (int idx = 0; idx < times; idx++)
            switch (idx % 5)
            {
                case 0: { var i = fixture.GetInputDTO(); i.Name = null!;
                    yield return new object[] { i, "Name should not be null or empty" }; break; }
                // ...
            }
    }
}
```

## Naming

| Element | Pattern |
|---------|---------|
| Test class | `{UseCase}Test` |
| Use case fixture | `{UseCase}TestFixture` |
| Collection class | `{UseCase}TestFixtureCollection` |
| Data generator | `{UseCase}TestDataGenerator` |
| Aggregate base fixture | `{Aggregate}UseCasesBaseFixture` |
| Test method | PascalCase verb describing expectation (`CreateCategory`, `ThrowWhenInputDTOIsInvalid`, `NotFoundExceptionWhenCategoryDoesNotExist`) |

## Common usings/aliases

```csharp
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using UseCasesAlias = Fc.Codeflix.Catalog.Application.UseCases.Category.Xxx;
```

## Coverage to write per use case

Happy path · invalid input scenarios via `[Theory]` · NotFound when repository throws · validation exception messages match `DomainValidation` standard messages.
