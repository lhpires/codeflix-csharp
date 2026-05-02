
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
    public readonly ListCategoriesTestFixture _fixture;

    public ListCategoriesTest(ListCategoriesTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(List))]
    [Trait("Application","ListCategories - Use Cases")]
    public async Task List()
    {
        var repositoryMock = _fixture.GetRepositoryMock();


        List<DomainEntity.Category> lstCategories = _fixture.GetCategoriesList();

        var input = _fixture.GetExampleInput();

        var outputRepositorySearch = new SearchOutputDTO<DomainEntity.Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<DomainEntity.Category>) lstCategories,
            total: (new Random()).Next(50,207)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategoriesUseCase(repositoryMock.Object);

        var execute = await useCase.Handle(input, CancellationToken.None);

        List<CategoryOutputDTO> returnedItems = ((List<CategoryOutputDTO>) execute.Items);


        execute.Should().NotBeNull();
        execute.Page.Should().Be(outputRepositorySearch.CurrentPage);
        execute.PerPage.Should().Be(outputRepositorySearch.PerPage);
        execute.Total.Should().Be(outputRepositorySearch.Total);
        execute.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        returnedItems.ForEach(outputItem =>
        {
            DomainEntity.Category repoCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id)!;

            repoCategory.Should().NotBeNull();
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repoCategory.Name);
            outputItem.Description.Should().Be(repoCategory.Description);
            outputItem.Id.Should().NotBeEmpty();
            outputItem.CreatedAt.Should().NotBeSameDateAs(default);
        });

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Theory(DisplayName = nameof(ListInputWithOutAllParameters))]
    [Trait("Application", "ListCategories - Use Cases")]
    [MemberData(
        nameof(ListCategoriesTestDataGenerator.GetInputWithoutAllParameters),
        parameters: 14,
        MemberType = typeof(ListCategoriesTestDataGenerator)
    )]
    public async Task ListInputWithOutAllParameters(ListCategoriesInputDTO input)
    {
        var repositoryMock = _fixture.GetRepositoryMock();


        List<DomainEntity.Category> lstCategories = _fixture.GetCategoriesList();

        var outputRepositorySearch = new SearchOutputDTO<DomainEntity.Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<DomainEntity.Category>)lstCategories,
            total: (new Random()).Next(50, 207)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategoriesUseCase(repositoryMock.Object);

        var execute = await useCase.Handle(input, CancellationToken.None);

        List<CategoryOutputDTO> returnedItems = ((List<CategoryOutputDTO>)execute.Items);


        execute.Should().NotBeNull();
        execute.Page.Should().Be(outputRepositorySearch.CurrentPage);
        execute.PerPage.Should().Be(outputRepositorySearch.PerPage);
        execute.Total.Should().Be(outputRepositorySearch.Total);
        execute.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        returnedItems.ForEach(outputItem =>
        {
            DomainEntity.Category repoCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id)!;

            repoCategory.Should().NotBeNull();
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repoCategory.Name);
            outputItem.Description.Should().Be(repoCategory.Description);
            outputItem.Id.Should().NotBeEmpty();
            outputItem.CreatedAt.Should().NotBeSameDateAs(default);
        });

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact(DisplayName = nameof(ListOkWhenEmpty))]
    [Trait("Application", "ListCategories - Use Cases")]
    public async Task ListOkWhenEmpty()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        List<DomainEntity.Category> lstCategories = _fixture.GetCategoriesList();

        var input = _fixture.GetExampleInput();

        IReadOnlyList<DomainEntity.Category> lstEmptyCategory = new List<DomainEntity.Category>().AsReadOnly();

        var outputRepositorySearch = new SearchOutputDTO<DomainEntity.Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: lstEmptyCategory,
            total: lstEmptyCategory.Count
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategoriesUseCase(repositoryMock.Object);

        var execute = await useCase.Handle(input, CancellationToken.None);

        List<CategoryOutputDTO> returnedItems = ((List<CategoryOutputDTO>)execute.Items);


        execute.Should().NotBeNull();
        execute.Page.Should().Be(outputRepositorySearch.CurrentPage);
        execute.PerPage.Should().Be(outputRepositorySearch.PerPage);
        execute.Total.Should().Be(0);
        execute.Items.Should().HaveCount(lstEmptyCategory.Count);

        returnedItems.ForEach(outputItem =>
        {
            DomainEntity.Category repoCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id)!;

            repoCategory.Should().NotBeNull();
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repoCategory.Name);
            outputItem.Description.Should().Be(repoCategory.Description);
            outputItem.Id.Should().NotBeEmpty();
            outputItem.CreatedAt.Should().NotBeSameDateAs(default);
        });

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInputDTO>(
                x =>
                x.Page == input.Page &&
                x.PerPage == input.PerPage &&
                x.Search == input.Search &&
                x.OrderBy == input.Sort &&
                x.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

}
