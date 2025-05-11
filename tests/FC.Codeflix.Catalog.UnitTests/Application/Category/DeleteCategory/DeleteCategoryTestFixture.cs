using Fc.Codeflix.Catalog.Application.Interfaces;
using Fc.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
{ }

public class DeleteCategoryTestFixture : BaseFixture
{
    public DeleteCategoryTestFixture() : base() { }

    public DeleteCategoryInputDTO GetInputDTO()
       =>
       new(
           Guid.NewGuid()
       );

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


    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();

}
