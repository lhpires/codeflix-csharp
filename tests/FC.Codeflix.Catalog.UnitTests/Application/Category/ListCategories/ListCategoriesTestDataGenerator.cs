using Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories
{
    public class ListCategoriesTestDataGenerator
    {
        public static IEnumerable<object[]> GetInputWithoutAllParameters(int times = 15)
        {
            ListCategoriesTestFixture fixture = new ListCategoriesTestFixture();

            var inputExample = fixture.GetExampleInput();

            int numbersOfScenarios = 7;

            for (int i = 0; i<times;i++)
            {
                switch(i % numbersOfScenarios)
                {
                    case 0:
                        yield return new object[] { new ListCategoriesInputDTO() };
                        break;
                    case 1:
                        yield return new object[] { new ListCategoriesInputDTO(inputExample.Page) };
                        break;
                    case 3:
                        yield return new object[] { new ListCategoriesInputDTO(inputExample.Page,inputExample.PerPage) };
                        break;
                    case 4:
                        yield return new object[] { new ListCategoriesInputDTO(inputExample.Page, inputExample.PerPage,inputExample.Search) };
                        break;
                    case 5:
                        yield return new object[] { new ListCategoriesInputDTO(inputExample.Page, inputExample.PerPage, inputExample.Search,inputExample.Sort) };
                        break;
                    case 6:
                        yield return new object[] { inputExample };
                        break;
                    default:
                        yield return new object[] { new ListCategoriesInputDTO() };
                        break;
                }
            }
        }
    }
}
