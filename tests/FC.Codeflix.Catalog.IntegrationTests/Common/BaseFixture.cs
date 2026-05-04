using Bogus;

namespace FC.Codeflix.Catalog.IntegrationTests.Common
{
    public abstract class BaseFixture
    {
        public Faker Faker { get; set; }

        public BaseFixture()
        {
            Faker = new Faker("pt_BR");
        }
    }
}
