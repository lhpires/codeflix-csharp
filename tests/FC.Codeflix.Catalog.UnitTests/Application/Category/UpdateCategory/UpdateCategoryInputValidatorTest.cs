using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;


[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GenerateErrorValidationWhenEmptyGuid))]
    [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
    public void GenerateErrorValidationWhenEmptyGuid()
    {
        var input = _fixture.GetValidInputDTO(Guid.Empty);

        var validator = new UpdateCategoryInputValidator();

        var execute = validator.Validate(input);


        execute.Should().NotBeNull();
        execute.IsValid.Should().BeFalse();
        execute.Errors.Should().HaveCount(1);
        execute.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }

    [Fact(DisplayName = nameof(ValidateOkWhenInputIsValid))]
    [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
    public void ValidateOkWhenInputIsValid()
    {
        var input = _fixture.GetValidInputDTO();

        var validator = new UpdateCategoryInputValidator();

        var execute = validator.Validate(input);

        execute.Should().NotBeNull();
        execute.IsValid.Should().BeTrue();
        execute.Errors.Should().HaveCount(0);
    }

}
