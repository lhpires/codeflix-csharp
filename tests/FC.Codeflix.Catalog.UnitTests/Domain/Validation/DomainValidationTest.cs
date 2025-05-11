
using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;

public class DomainValidationTest
{
    public Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "Validation")]
    public void NotNullOk()
    {
        string value = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.NotNull(value, fieldName);
        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "Validation")]
    public void NotNullThrowWhenNull()
    {
        string value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.NotNull(value, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "Validation")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null or empty");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "Validation")]
    public void NotNullOrEmptyOk()
    {
        string target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "Validation")]
    [MemberData(nameof(GetValuesLessThanMin), parameters: 10)]
    public void MinLengthThrowWhenLess(string target,int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} cannot be less than {minLength} characters long");

    }

    public static IEnumerable<object[]> GetValuesLessThanMin(int timesToExecute)
    {
        var Faker = new Faker();
        for (int i = 0; i < timesToExecute; i++)
        {
            string productName = Faker.Commerce.ProductName();
            int minLength = productName.Length + new Random().Next(1, 20);
            yield return new object[] { productName, minLength };
        }
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "Validation")]
    [MemberData(nameof(GetValuesGreaterOrEqualThanMin), parameters: 10)]
    public void MinLengthOk(string target,int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().NotThrow();

    }

    public static IEnumerable<object[]> GetValuesGreaterOrEqualThanMin(int timesToExecute)
    {
        yield return new object[] { "1234567", 7 };

        var Faker = new Faker();
        for (int i = 0; i < (timesToExecute - 1); i++)
        {
            string productName = Faker.Commerce.ProductName();
            int minLength = productName.Length - new Random().Next(1, 7);
            yield return new object[] { productName, minLength };
        }
    }

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target,int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ","");

        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} cannot have more than {maxLength} characters long");
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMax(int timesToExecute)
    {
        yield return new object[] { "1234567", 6 };

        var Faker = new Faker("pt_BR");

        for (int i = 0; i < (timesToExecute - 1); i++)
        {
            string productName = Faker.Commerce.ProductName();
            int maxLength = productName.Length - new Random().Next(1, 10);
            yield return new object[] { productName, maxLength };
        }

    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "Validation")]
    [MemberData(nameof(GetValuesLessOrEqualThanMax), parameters: 10)]
    public void MaxLengthOk(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesLessOrEqualThanMax(int timesToExecute)
    {
        yield return new object[] { "1234567", 7 };

        var Faker = new Faker("pt_BR");

        for (int i = 0; i < (timesToExecute - 1); i++)
        {
            string productName = Faker.Commerce.ProductName();
            int maxLength = productName.Length + new Random().Next(1, 10);
            yield return new object[] { productName, maxLength };
        }

    }

}
