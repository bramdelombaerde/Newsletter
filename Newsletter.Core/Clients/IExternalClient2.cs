namespace Newsletter.Core.Clients
{
    public interface IExternalClient2
    {
        Task<bool> SendNewsletter(Domain.Newsletter newsletter);
    }
}
