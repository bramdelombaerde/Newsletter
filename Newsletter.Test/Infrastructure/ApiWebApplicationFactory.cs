using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newsletter.Api;
using Newsletter.Api.Infrastructure;
using Newsletter.Infrastructure.Persistence;

namespace Newsletter.Test.Infrastructure
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime, IDisposable
    {
        public IConfiguration Configuration { get; }
        public NewsletterDatastore Context => Services.GetService<NewsletterDatastore>();
        public HttpClient Client => CreateClient();

        public ApiWebApplicationFactory()
        {
        }
        public async Task InitializeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            Context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);

            await Context.Database.EnsureDeletedAsync();
        }

        protected override IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddHttpContextAccessor();
            services.AddCustomServices();
            services.AddClients();

            services.AddDbContext<NewsletterDatastore>(options => options.UseInMemoryDatabase($"InMemoryNewsletterDB-{Guid.NewGuid()}")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)), ServiceLifetime.Singleton);
        })
        .ConfigureWebHost(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            //webBuilder.UseConfiguration(Configuration);
            webBuilder.UseTestServer();
        })
        .UseEnvironment("IntegrationTesting");
    }
}
