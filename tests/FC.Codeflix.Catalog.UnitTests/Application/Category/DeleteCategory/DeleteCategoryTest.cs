using Fc.Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCasesAlias = Fc.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.DeleteCategory;


[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    DeleteCategoryTestFixture _fixture;
    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("Application", "DeleteCategory - UseCases")]
    public async Task DeleteCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var validCategoryEntity = _fixture.InstantiateValidCategory();
        var input = new UseCasesAlias.DeleteCategoryInputDTO(validCategoryEntity.Id);

        repositoryMock
        .Setup(x =>
            x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(validCategoryEntity);




        var useCase = new UseCasesAlias.DeleteCategoryUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var execute = await useCase.Handle(input, CancellationToken.None);


        repositoryMock.Verify(x => x.Get(
            validCategoryEntity.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);

        repositoryMock.Verify(x => x.Delete(
             validCategoryEntity,
             It.IsAny<CancellationToken>()
         ), Times.Once);


        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }

    [Fact(DisplayName = nameof(ThrownWhenCategoryNotFound))]
    [Trait("Application", "DeleteCategory - UseCases")]
    public async Task ThrownWhenCategoryNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var validCategoryEntity = _fixture.InstantiateValidCategory();
        var input = new UseCasesAlias.DeleteCategoryInputDTO(validCategoryEntity.Id);

        repositoryMock
        .Setup(x =>
            x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new NotFoundException($"Category {validCategoryEntity.Id} not found"));




        var useCase = new UseCasesAlias.DeleteCategoryUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var execute = async () => await useCase.Handle(input, CancellationToken.None);


        await execute.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(x => x.Get(
            validCategoryEntity.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

}
