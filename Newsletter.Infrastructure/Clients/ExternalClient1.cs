using Newsletter.Core.Clients;

namespace Newsletter.Infrastructure.Services
{
    public class ExternalClient1 : IExternalClient1
    {
        private readonly HttpClient _httpClient;

        public ExternalClient1(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendNewsletter(Domain.Newsletter newsletter) => await Task.FromResult(true);
    }
}
