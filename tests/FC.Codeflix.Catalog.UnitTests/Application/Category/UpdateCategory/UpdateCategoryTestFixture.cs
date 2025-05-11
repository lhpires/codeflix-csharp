
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Fc.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;
using Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }

public class UpdateCategoryTestFixture :  BaseFixture
{
    public UpdateCategoryTestFixture() : base() {}

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

    public UpdateCategoryInputDTO GetValidInputDTO(Guid? id = null)
    {
        return new UpdateCategoryInputDTO(
            id ?? Guid.NewGuid(),
            this.GetValidCategoryName(),
            this.GetValidDescription(),
            this.GetRandomBoolean()
        );
    }

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();

}
