namespace Newsletter.Test.Infrastructure
{
    public abstract class TestBase
    {
        public TestBase()
        {
            Fixture = new ApiWebApplicationFactory();
        }

        public ApiWebApplicationFactory Fixture { get; }
    }
}
