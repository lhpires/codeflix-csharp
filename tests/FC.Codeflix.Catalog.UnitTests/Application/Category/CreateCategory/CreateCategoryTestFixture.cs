using Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.UnitTests.Application.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{ }

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public CreateCategoryTestFixture() : base() { }

    public CreateCategoryInputDTO GetInputDTO()
        =>
        new(
            GetValidCategoryName(),
            GetValidDescription(),
            GetRandomBoolean()
        );

}
