using Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.UnitTests.Application.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    public ListCategoriesTestFixture() : base() { }

    public List<DomainEntity.Category> GetCategoriesList(int length = 10)
    {
        List<DomainEntity.Category> lst = new();

        for (int i = 0; i < length; i++)
        {
            lst.Add( this.GetValidCategory() );
        }

        return lst;
    }

    public ListCategoriesInputDTO GetExampleInput()
    {
        Random random = new Random();

        return new ListCategoriesInputDTO
        (
            page: random.Next(1,10),
            perPage: random.Next(15,100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0,10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
        );
    }

}
