namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;


[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private string validName = "Valid Name Category";
    private string validDescription = "Valid Description Category";

    private readonly CategoryTestFixture _categoryTestFixture;
    public CategoryTest()
    {
        _categoryTestFixture = new CategoryTestFixture();
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain","Category - Aggregates")]
    public void Instantiate()
    {
        // Arrange
        var dateTimeBefore = DateTime.Now;

        // ACT
        DomainEntity.Category category = _categoryTestFixture.InstantiateValidCategory();

        var dateTimeAfter = DateTime.Now.AddSeconds(1);

        // ASSERT
        Assert.NotNull(category);
        category.Should().NotBeNull();


        //Assert.Equal(validName, category.Name);
        //Assert.Equal("Category Description", category.Description);
        category.Name.Length.Should().BeGreaterThanOrEqualTo(3);

        //Assert.NotEqual(default(Guid), category.Id); // pode ser Guid.Empty
        category.Id.Should().NotBeEmpty();

        // Assert.NotEqual(default(DateTime), category.CreatedAt);
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

        // Assert.True(category.CreatedAt > dateTimeBefore);
        (category.CreatedAt >= dateTimeBefore).Should().BeTrue();

        //Assert.True(category.CreatedAt < dateTimeAfter);
        (category.CreatedAt <= dateTimeAfter).Should().BeTrue();

        //Assert.True(category.IsActive);
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        // Arrange
        var validData = _categoryTestFixture.InstantiateValidCategory();

        var dateTimeBefore = DateTime.Now;

        // ACT
        var category = new DomainEntity.Category(validData.Name, validData.Description,isActive);

        var dateTimeAfter = DateTime.Now.AddSeconds(1);

        // ASSERT
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt >= dateTimeBefore);
        Assert.True(category.CreatedAt <= dateTimeAfter);
        Assert.Equal(isActive,category.IsActive);
    }

    [Theory(DisplayName = nameof(ErrorWhenNameIsEmptyOrNull))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void ErrorWhenNameIsEmptyOrNull(string? Name)
    {
        //Action action = () => new DomainEntity.Category(name!, "Valid Description");
        // Segunda de action
        void action() => new DomainEntity.Category(Name!, "Valid Description");

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Name)} should not be null or empty",exception.Message);
    }

    [Theory(DisplayName = nameof(ErrorWhenDescriptionIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(null)]
    public void ErrorWhenDescriptionIsEmpty(string? Description)
    {
        void action() => new DomainEntity.Category("Banco BTG", Description!);


        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal($"{nameof(Description)} should not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(ErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNamesWithLassThan3Characters),parameters: 10)]
    public void ErrorWhenNameIsLessThan3Characters(string Name)
    {
        Action action = () => new DomainEntity.Category(Name, validDescription);

        //var exception = Assert.Throws<EntityValidationException>(action);
        //Assert.Equal($"{nameof(Name)} cannot be less than 3 characters long", exception.Message);
        action.Should().Throw<EntityValidationException>().WithMessage($"{nameof(Name)} cannot be less than 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesWithLassThan3Characters(int timesToExecute)
    {
        var fixture = new CategoryTestFixture();
        for (int i = 0; i < timesToExecute; i ++)
        {
            var isOdd = i % 2 == 0;
            yield return new object[] {
                fixture.GetValidCategoryName().Substring(0, isOdd ? 1 : 2)
            };
        }
    }

    [Fact(DisplayName = nameof(ErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void ErrorWhenNameIsGreaterThan255Characters()
    {
        string Name = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(Name, validDescription);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Name)} cannot have more than 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(ErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void ErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        string Description = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "7").ToArray());

        Action action = () => new DomainEntity.Category(validName, Description);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Description)} cannot have more than 10000 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        var category = InstantiateValidCategory();
        category.Activate();
        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        var category = InstantiateValidCategory();
        category.Deactivate();
        Assert.False(category.IsActive);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {   
        //var category = InstantiateValidCategory();
        var category = _categoryTestFixture.InstantiateValidCategory();
        var categoryWithNewValues = _categoryTestFixture.InstantiateValidCategory();

        category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

        Assert.Equal(categoryWithNewValues.Name, category.Name);
        Assert.Equal(categoryWithNewValues.Description, category.Description);

    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateOnlyName()
    {
        var category = InstantiateValidCategory();
        var newName = _categoryTestFixture.GetValidCategoryName();

        var currentDescription = category.Description;

        category.Update(newName);

        Assert.Equal(newName, category.Name);
        Assert.Equal(currentDescription, category.Description);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmptyOrNull))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void UpdateErrorWhenNameIsEmptyOrNull(string? Name)
    {
        var category = InstantiateValidCategory();

        Action action = () => category.Update(Name!);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Name)} should not be null or empty", exception.Message);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("7A")]
    [InlineData("B7")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string Name)
    {
        var category = InstantiateValidCategory();

        Action action = () => category.Update(Name);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Name)} cannot be less than 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var category = InstantiateValidCategory();

        string Name = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

        Action action = () => category.Update(Name);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Name)} cannot have more than 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var category = InstantiateValidCategory();

        string Description = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "7").ToArray());

        Action action = () => category.Update(validName,Description);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal($"{nameof(Description)} cannot have more than 10000 characters long", exception.Message);
    }



    private DomainEntity.Category InstantiateValidCategory(string Name="",string Description="")
    {
        string finalName = (String.IsNullOrWhiteSpace(Name)) ? validName : Name;
        string finalDescription = (String.IsNullOrWhiteSpace(Description)) ? validDescription : Description;

        var validData = new
        {
            Name = finalName,
            Description = finalDescription,
            IsActive = true
        };

        return new DomainEntity.Category(validData.Name, validData.Description, validData.IsActive);
    }
}
