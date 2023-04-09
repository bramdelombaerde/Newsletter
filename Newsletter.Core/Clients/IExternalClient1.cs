namespace Newsletter.Core.Clients
{
    public interface IExternalClient1
    {
        Task<bool> SendNewsletter(Domain.Newsletter newsletter);
    }
}
