using Fc.Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using UseCases = Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    public readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Application", "GetCategory - UseCases")]
    public async void GetCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        DomainEntity.Category validCategory = _fixture.GetValidCategory();

        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )
        ).ReturnsAsync(validCategory);

        UseCases.GetCategoryInputDTO input = new(validCategory.Id);
        UseCases.GetCategoryUseCase useCase = new(
            repositoryMock.Object
        );

        var execute = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

        execute.Should().NotBeNull();
        execute.Name.Should().Be(validCategory.Name);
        execute.Description.Should().Be(validCategory.Description);
        execute.IsActive.Should().Be(true);
        execute.Id.Should().NotBeEmpty();
        execute.CreatedAt.Should().NotBeSameDateAs(default);

    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesNotExist))]
    [Trait("Application", "GetCategory - UseCases")]
    public async void NotFoundExceptionWhenCategoryDoesNotExist()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        Guid id = Guid.NewGuid();

        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )
        ).ThrowsAsync(
            new NotFoundException($"Category {id} not found")
        );

        UseCases.GetCategoryInputDTO input = new(id);
        UseCases.GetCategoryUseCase useCase = new(
            repositoryMock.Object
        );

        //var execute = await useCase.Handle(input, CancellationToken.None);
        var execute = async () => await useCase.Handle(input, CancellationToken.None);

        await execute.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }
}
