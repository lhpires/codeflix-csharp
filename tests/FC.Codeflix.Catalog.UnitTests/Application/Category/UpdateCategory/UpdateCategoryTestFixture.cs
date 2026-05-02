using Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.UnitTests.Application.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }

public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryTestFixture() : base() {}

    public UpdateCategoryInputDTO GetValidInputDTO(Guid? id = null)
    {
        return new UpdateCategoryInputDTO(
            id ?? Guid.NewGuid(),
            this.GetValidCategoryName(),
            this.GetValidDescription(),
            this.GetRandomBoolean()
        );
    }
}
