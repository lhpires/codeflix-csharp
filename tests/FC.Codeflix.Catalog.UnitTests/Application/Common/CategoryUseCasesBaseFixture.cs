using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Fc.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Common
{
    public abstract class CategoryUseCasesBaseFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWork() => new();

        public DomainEntity.Category GetValidCategory(string name = "", string description = "",bool isActive = true)
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
