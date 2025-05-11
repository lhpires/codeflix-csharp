using FluentAssertions;
using Moq;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using UseCasesAlias = Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Exceptions;
using Bogus;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;
    public CreateCategoryTest(CreateCategoryTestFixture fixture) => _fixture = fixture;


    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - UseCases")]
    public async void CreateCategory()
    {
        var repoitoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new CreateCategoryUseCase(
            repoitoryMock.Object,
            unitOfWorkMock.Object
        );

        // Carregando DTOS para chamada do Handle
        var input = _fixture.GetInputDTO();
        input.IsActive = false;


        // Executando UseCase
        // 1 - Como esse processo é assíncrono, caso precisemos cancelar a execução do processo, aqui no C# utilizamos o cancelattion token (encerra as threads da execução da aplicação)

        var execute = await useCase.Handle(input, CancellationToken.None); // Como esse useCase lida com banco de dados, a chamada dele precisa ser assíncrona


        execute.Should().NotBeNull();
        execute.Name.Should().Be(input.Name);
        execute.Description.Should().Be(input.Description);
        execute.IsActive.Should().Be(false);
        execute.Id.Should().NotBeEmpty();
        execute.CreatedAt.Should().NotBeSameDateAs(default);

        // VERIFICAR SE DENTRO DO MOCK FOI CHAMADO O MÉTODO CREATE COM UMA ENTIDADE DE CATEGORIA COMO PARÂMETRO
        repoitoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<DomainEntity.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameProp))]
    [Trait("Application", "CreateCategory - UseCases")]
    public async void CreateCategoryWithOnlyNameProp()
    {
        var repoitoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new CreateCategoryUseCase(
            repoitoryMock.Object,
            unitOfWorkMock.Object
        );

        string validCategoryName = _fixture.GetValidCategoryName();

        // Carregando DTOS para chamada do Handle
        var input = new CreateCategoryInputDTO(
            validCategoryName
        );


        // Executando UseCase
        // 1 - Como esse processo é assíncrono, caso precisemos cancelar a execução do processo, aqui no C# utilizamos o cancelattion token (encerra as threads da execução da aplicação)
        var execute = await useCase.Handle(input, CancellationToken.None); // Como esse useCase lida com banco de dados, a chamada dele precisa ser assíncrona


        execute.Should().NotBeNull();
        execute.Name.Should().Be(validCategoryName);
        execute.Description.Should().Be("");
        execute.IsActive.Should().Be(true);
        execute.Id.Should().NotBeEmpty();
        execute.CreatedAt.Should().NotBeSameDateAs(default);

        // VERIFICAR SE DENTRO DO MOCK FOI CHAMADO O MÉTODO CREATE COM UMA ENTIDADE DE CATEGORIA COMO PARÂMETRO
        repoitoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<DomainEntity.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

    }


    [Theory(DisplayName = nameof(ThrowWhenInputDTOIsInvalid))]
    [Trait("Application", "CreateCategory - UseCases")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputsDtos),
        parameters: 15,
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async void ThrowWhenInputDTOIsInvalid(CreateCategoryInputDTO Invlalidinput, string exceptionMessage)
    {
        var repoitoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWork();

        var useCase = new CreateCategoryUseCase(
            repoitoryMock.Object,
            unitOfWorkMock.Object
        );


        Func<Task> task = async () => await useCase.Handle(Invlalidinput, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>().WithMessage(exceptionMessage);

    }
}
