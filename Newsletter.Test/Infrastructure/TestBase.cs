using Newsletter.Api.Models.Titel;

namespace Newsletter.Test.Infrastructure
{
    public abstract class TestBase
    {
        public TestBase()
        {
            Fixture = new ApiWebApplicationFactory();
        }

        public ApiWebApplicationFactory Fixture { get; }

        public CreateTitel GenerateCreateTitel()
        {
            return new CreateTitel()
            {
                Name = "Nieuwsblad",
                ShortName = "NB",
            };
        }
    }
}
