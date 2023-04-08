using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Newsletter.Core.Helper;
//using System.Net.Mail;

namespace Newsletter.Infrastructure.Helper;
public class EmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;

    public EmailSender()
    {
        //_smtpClient = new SmtpClient("smtp.gmail.com", 587)
        //{
        //    Credentials = new NetworkCredential("newsletterTestMediahuis@gmail.com", "@Test1234"),
        //};
    }

    public async Task SendEmailAsync(string toAddress, string subject, string body)
    {
        //var message = new MailMessage("newsletterTestMediahuis@gmail.com", toAddress, subject, body);
        //await _smtpClient.SendMailAsync(message);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("donotreply@mediahuis.com"));
        email.To.Add(MailboxAddress.Parse(toAddress));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("newslettertestmediahuis@gmail.com", "fagksgzcdqftnymu");
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
