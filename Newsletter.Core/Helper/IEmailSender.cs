namespace Newsletter.Core.Helper
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string body);
    }
}
