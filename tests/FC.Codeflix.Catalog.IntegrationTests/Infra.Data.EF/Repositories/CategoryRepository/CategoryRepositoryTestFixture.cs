using Fc.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Infra.Data.EF;
using FC.Codeflix.Catalog.IntegrationTests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection 
        : ICollectionFixture<CategoryRepositoryTestFixture>
    { }

    public class CategoryRepositoryTestFixture 
        : BaseFixture
    {
        public Mock<ICategoryRepository> GetRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWork() => new();

        public CodeflixCatalogDbContext CreateDbContext()
        {
            CodeflixCatalogDbContext dbContext = new CodeflixCatalogDbContext(
                new DbContextOptionsBuilder<CodeflixCatalogDbContext>().UseInMemoryDatabase("integration-test-db").Options
            );

            return dbContext;
        }

        public DomainEntity.Category GetValidCategory(string name = "", string description = "", bool isActive = true)
        {
            string finalName = (String.IsNullOrWhiteSpace(name)) ? GetValidCategoryName() : name;
            string finalDescription = (string.IsNullOrEmpty(description)) ? GetValidDescription() : description;

            var validData = new
            {
                Name = finalName,
                Description = finalDescription,
                IsActive = isActive
            };

            return new DomainEntity.Category(validData.Name, validData.Description, validData.IsActive);
        }

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
    }
}
