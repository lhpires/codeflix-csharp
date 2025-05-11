
using FluentAssertions;
using UseCasesAlias = Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Moq;
using Fc.Codeflix.Catalog.Application.Exceptions;
using Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    public readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 20,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async void UpdateCategory(DomainEntity.Category validCategoryEntity, UseCasesAlias.UpdateCategoryInputDTO input)
    {
        var repository = _fixture.GetRepositoryMock();
        var unitOfWork = _fixture.GetUnitOfWork();     

        repository.Setup(x => x.Get(
            validCategoryEntity.Id,
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(validCategoryEntity);

        UseCasesAlias.UpdateCategoryUseCase useCase = new UseCasesAlias.UpdateCategoryUseCase(
            repository.Object,
            unitOfWork.Object
        );


        var execute = await useCase.Handle(input,CancellationToken.None);

        execute.Should().NotBeNull();
        execute.Name.Should().Be(input.Name);
        execute.Description.Should().Be(input.Description);
        execute.IsActive.Should().Be(input.IsActive);

        repository.Verify(x => x.Get(
            validCategoryEntity.Id,
            It.IsAny<CancellationToken>()
        ),Times.Once);

        repository.Verify(x => x.Update(
            validCategoryEntity,
            It.IsAny<CancellationToken>()
        ));


        unitOfWork.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }

    [Fact(DisplayName = nameof(ThrowNotFoundUpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCases")]   
    public async void ThrowNotFoundUpdateCategory()
    {
        var repository = _fixture.GetRepositoryMock();
        var unitOfWork = _fixture.GetUnitOfWork();

        var input = _fixture.GetValidInputDTO();

        repository.Setup(x => x.Get(
            input.Id,
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(new NotFoundException($"category {input.Id} not found"));

        UseCasesAlias.UpdateCategoryUseCase useCase = new UseCasesAlias.UpdateCategoryUseCase(
            repository.Object,
            unitOfWork.Object
        );

        var task = async()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repository.Verify(x => x.Get(
            input.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }

    [Theory(DisplayName = nameof(ThrowWhenCantUpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetInvalidInputsDtos),
        parameters: 12,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async void ThrowWhenCantUpdateCategory(UpdateCategoryInputDTO input,string expectedExceptionMessage)
    {
        DomainEntity.Category validCategory = _fixture.GetValidCategory();

        var repository = _fixture.GetRepositoryMock();
        var unitOfWork = _fixture.GetUnitOfWork();

        repository.Setup(x => x.Get(
            input.Id,
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(validCategory);

        UseCasesAlias.UpdateCategoryUseCase useCase = new UseCasesAlias.UpdateCategoryUseCase(
            repository.Object,
            unitOfWork.Object
        );

        var task = async() 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>().WithMessage(expectedExceptionMessage);

        repository.Verify(x => x.Get(
            input.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);

    }
}
