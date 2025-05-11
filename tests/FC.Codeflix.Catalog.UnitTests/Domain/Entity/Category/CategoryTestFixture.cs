
namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

using FC.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

public class CategoryTestFixture: BaseFixture
{
    public CategoryTestFixture() : base() {}

    public string GetValidCategoryName()
    {
        string validName = "";

        while(validName.Length < 3)
        {
            validName = Faker.Commerce.ProductName();
        }

        return validName;
    }

    public string GetValidDescription()
    {
        string validDescription = string.Empty;

        while(string.IsNullOrEmpty(validDescription) || validDescription.Length > 10000)
        {
            validDescription = Faker.Commerce.ProductDescription();
        }

        return validDescription;
    }

    public DomainEntity.Category InstantiateValidCategory(string Name = "", string Description = "")
    {
        string finalName = (String.IsNullOrWhiteSpace(Name)) ? GetValidCategoryName() : Name;
        string finalDescription = (string.IsNullOrEmpty(Description)) ? GetValidDescription() : Description;

        var validData = new
        {
            Name = finalName,
            Description = finalDescription,
            IsActive = true
        };

        return new DomainEntity.Category(validData.Name, validData.Description, validData.IsActive);
    }
}

// 1 - JEITO DO XUNIT, de permitir a injeção de dependencia em nossa classe principal
[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryFixtureCollection : ICollectionFixture<CategoryTestFixture>
{ }