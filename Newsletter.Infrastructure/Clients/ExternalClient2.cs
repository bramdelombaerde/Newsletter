using Newsletter.Core.Clients;

namespace Newsletter.Infrastructure.Services
{
    public class ExternalClient2 : IExternalClient2
    {
        private readonly HttpClient _httpClient;

        public ExternalClient2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendNewsletter(Domain.Newsletter newsletter) => await Task.FromResult(true);
    }
}
