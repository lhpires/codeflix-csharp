
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Fc.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }

public class ListCategoriesTestFixture : BaseFixture
{
    public ListCategoriesTestFixture() : base() { }

    public string GetValidCategoryName()
    {
        string validName = "";

        while (validName.Length < 3)
        {
            validName = Faker.Commerce.ProductName();
        }

        return validName;
    }

    public string GetValidDescription()
    {
        string validDescription = string.Empty;

        while (string.IsNullOrEmpty(validDescription) || validDescription.Length > 10000)
        {
            validDescription = Faker.Commerce.ProductDescription();
        }

        return validDescription;
    }

    public bool GetRandomBoolean() => new Random().NextDouble() < 0.5;

    public DomainEntity.Category GetValidCategory()
     => new(
         GetValidCategoryName(),
         GetValidDescription(),
         GetRandomBoolean()
     );

    public List<DomainEntity.Category> GetCategoriesList(int length = 10)
    {
        List<DomainEntity.Category> lst = new();

        for (int i = 0; i < length; i++)
        {
            lst.Add( GetValidCategory() );
        }

        return lst;
    }

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();

}
