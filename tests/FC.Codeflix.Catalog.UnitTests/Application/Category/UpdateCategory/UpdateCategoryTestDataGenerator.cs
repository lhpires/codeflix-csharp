
using Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times)
    {
        var fixture = new UpdateCategoryTestFixture();


        for (int idx = 0; idx < times; idx++)
        {
            DomainEntity.Category validCategoryEntity = fixture.GetValidCategory();

            var input = fixture.GetValidInputDTO(validCategoryEntity.Id);

            yield return new object[] { validCategoryEntity,input };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputsDtos(int times)
    {
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputsDtos = new List<object[]>();

        int totalInvalidCases = 3;

        for (int idx = 0; idx < times; idx++)
        {
            switch (idx % totalInvalidCases)
            {
                case 1:

                    // SHORTNAME
                    UpdateCategoryInputDTO inputDtoShortName = fixture.GetValidInputDTO();
                    inputDtoShortName.Name = inputDtoShortName.Name.Substring(0, 2);
                    invalidInputsDtos.Add(
                        new object[] { inputDtoShortName, $"Name cannot be less than 3 characters long" }
                    );
                    break;

                case 2:

                    // LONG NAME GREATER THAN 255 CHARACTERS
                    UpdateCategoryInputDTO inputDtoLongName = fixture.GetValidInputDTO();
                    string invalidName = fixture.Faker.Commerce.ProductDescription();
                    while (string.IsNullOrEmpty(invalidName) || invalidName.Length < 255)
                    {
                        invalidName = $"{invalidName} {fixture.Faker.Commerce.ProductDescription()}";
                    }
                    inputDtoLongName.Name = invalidName;

                    invalidInputsDtos.Add(
                        new object[] { inputDtoLongName, $"Name cannot have more than 255 characters long" }
                    );

                    break;
                case 3:
                    // DESCRIPTION TOO LONG
                    UpdateCategoryInputDTO inputDtoLongDescription = fixture.GetValidInputDTO();
                    inputDtoLongDescription.Description = fixture.Faker.Commerce.ProductDescription();
                    while (inputDtoLongDescription.Description.Length < 10000)
                        inputDtoLongDescription.Description = $"{inputDtoLongDescription.Description} {fixture.Faker.Commerce.ProductDescription()}";

                    invalidInputsDtos.Add(
                        new object[] { inputDtoLongDescription, "Description cannot have more than 10000 characters long" }
                    );
                    break;

                default:
                    break;
            }
        }

        return invalidInputsDtos;
    }
}
