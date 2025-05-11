

using Fc.Codeflix.Catalog.Application.Interfaces;
using Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{ }

public class CreateCategoryTestFixture : BaseFixture
{
    public CreateCategoryTestFixture() : base() { }

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

    public CreateCategoryInputDTO GetInputDTO()
        =>
        new(
            GetValidCategoryName(),
            GetValidDescription(),
            GetRandomBoolean()
        );

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWork() => new();

}
