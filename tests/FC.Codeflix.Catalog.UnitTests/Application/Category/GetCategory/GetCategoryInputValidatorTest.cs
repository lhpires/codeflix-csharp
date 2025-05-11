
using Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputValidatorTest
{
    public readonly GetCategoryTestFixture _fixture;
    public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ValidatorOk))]
    [Trait("Application", "GetCategoryInputValidator - UseCases")]
    public void ValidatorOk()
    {
        Guid id = Guid.NewGuid();

        var validInput = new GetCategoryInputDTO(id);

        var validator = new GetCategoryInputValidator();

        var executeValidation = validator.Validate(validInput);

        executeValidation.Should().NotBeNull();
        executeValidation.IsValid.Should().BeTrue();
        executeValidation.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(InvlaidWhenEmptyGuidId))]
    [Trait("Application", "GetCategoryInputValidator - UseCases")]
    public void InvlaidWhenEmptyGuidId()
    {
        Guid id = Guid.Empty;

        var invalidInput = new GetCategoryInputDTO(id);

        var validator = new GetCategoryInputValidator();

        var executeValidation = validator.Validate(invalidInput);

        executeValidation.Should().NotBeNull();
        executeValidation.IsValid.Should().BeFalse();
        executeValidation.Errors.Should().HaveCountGreaterThan(0);
    }
}
