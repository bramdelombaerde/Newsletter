using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Newsletter.Appconfig;
using Newsletter.Core.Helper;

namespace Newsletter.Infrastructure.Helper;
public class EmailSender : IEmailSender
{
    private readonly IOptions<MailSettings> _settings;

    public EmailSender(IOptions<MailSettings> settings)
    {
        _settings = settings;
    }

    public async Task SendEmailAsync(string toAddress, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_settings.Value.From));
        email.To.Add(MailboxAddress.Parse(toAddress));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect(_settings.Value.Host, _settings.Value.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_settings.Value.UserName, _settings.Value.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
