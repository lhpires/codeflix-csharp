
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture> { }

public class GetCategoryTestFixture : BaseFixture
{
    public GetCategoryTestFixture() : base() { }

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

    public DomainEntity.Category GetValidCategory()
        => new(
            GetValidCategoryName(),
            GetValidDescription(),
            true
        );


    public Mock<ICategoryRepository> GetRepositoryMock() => new();
}
