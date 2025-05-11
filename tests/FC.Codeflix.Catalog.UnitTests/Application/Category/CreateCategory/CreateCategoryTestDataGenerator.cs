
using Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputsDtos(int times)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputsDtos = new List<object[]>();

        int totalInvalidCases = 5;

        for (int idx = 0; idx < times; idx++)
        {
            switch (idx % totalInvalidCases)
            {
                case 1:
                    // NAME CANNOT BE NULL
                    CreateCategoryInputDTO inputDtoNullName = fixture.GetInputDTO();
                    inputDtoNullName.Name = null!;
                    invalidInputsDtos.Add(
                        new object[] { inputDtoNullName, $"Name should not be null or empty" }
                    );
                    break;

                case 2:
                    // SHORTNAME
                    CreateCategoryInputDTO inputDtoShortName = fixture.GetInputDTO();
                    inputDtoShortName.Name = inputDtoShortName.Name.Substring(0, 2);
                    invalidInputsDtos.Add(
                        new object[] { inputDtoShortName, $"Name cannot be less than 3 characters long" }
                    );
                    break;

                case 3:
                    // LONG NAME GREATER THAN 255 CHARACTERS
                    CreateCategoryInputDTO inputDtoLongName = fixture.GetInputDTO();
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
                case 4:
                    // DESCRIPTION CANNOT BE NULL
                    CreateCategoryInputDTO inputDtoNullDescription = fixture.GetInputDTO();
                    inputDtoNullDescription.Description = null!;
                    invalidInputsDtos.Add(
                        new object[] { inputDtoNullDescription, $"Description should not be null" }
                    );
                    break;
                default:
                    // cannot have more than 10000 characters long
                    CreateCategoryInputDTO inputDtoLongDescription = fixture.GetInputDTO();
                    inputDtoLongDescription.Description = fixture.Faker.Commerce.ProductDescription();
                    while (inputDtoLongDescription.Description.Length < 10000)
                        inputDtoLongDescription.Description = $"{inputDtoLongDescription.Description} {fixture.Faker.Commerce.ProductDescription()}";

                    invalidInputsDtos.Add(
                        new object[] { inputDtoLongDescription, "Description cannot have more than 10000 characters long" }
                    );
                    break;
            }

        }

        return invalidInputsDtos;
    }
}
